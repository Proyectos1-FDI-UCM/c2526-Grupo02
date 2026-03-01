//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo - JESUS DIEZ A PARTIR DE CODIGO DE ALEJANDRO
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/// <summary>
/// Inventario del jugador usando Input System directamente.
/// - Navegación toroide con teclado o gamepad
/// - Bloquea movimiento del jugador cuando está abierto
/// - Slots dinámicos con resaltado
/// - Añadir items de prueba con tecla T // funcion "comentada"
/// - Sin corutinas ni lambdas
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private GameObject invHud;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private int columns = 5;

    [Header("Inventario")]
    [SerializeField] private int maxItems = 10;

    [Header("Input System")]
    [SerializeField] private InputActionAsset inputActions; // arrastra aquí tu Input Actions Asset
    [SerializeField] private float toggleDelay = 0.3f;

    private float navCooldown = 0.2f;   // segundos entre movimientos
    private float navTimer = 0f;

    private float lastToggleTime = 0f;
    private InventoryItem[] _inv;
    private int _nObj = 0;

    private Image[] slotImages;
    private int _selectedIndex = 0;

    private bool isOpen = false;
    public bool IsOpen => isOpen;

    // Referencias a las acciones
    private InputAction toggleInventoryAction;
    private InputAction navigateAction;
    private InputAction addTestItemAction;
    private InputAction pickupItemAction;

    private void Start()
    {
        // Inicializar inventario
        _inv = new InventoryItem[maxItems];
        slotImages = new Image[maxItems];

        // Crear slots en la UI
        for (int i = 0; i < maxItems; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotsParent);
            Image img = slot.GetComponent<Image>();
            img.enabled = false;
            slotImages[i] = img;
        }

        invHud.SetActive(isOpen);

        // Buscar las acciones por nombre dentro del InputActionAsset
        toggleInventoryAction = inputActions.FindAction("ToggleInventory");
        navigateAction = inputActions.FindAction("Navigate");
        //addTestItemAction = inputActions.FindAction("AddTestItem");
        pickupItemAction = inputActions.FindAction("Pickup");
    }

    
    private void Update()
    {
        HandleToggleInventory();

        if (!isOpen)
        {
            return; // bloquea navegación y acciones si inventario cerrado
        }

        //HandleAddTestItem();
        HandleNavigation();
    }

    #region Inputs

    private void HandleToggleInventory()
    {
        // Si no ha pasado suficiente tiempo desde la última pulsación, no hacemos nada
        if (Time.time - lastToggleTime < toggleDelay)
            return;

        if (toggleInventoryAction.ReadValue<float>() > 0.1f)
        {
            // Cambiar estado del inventario
            isOpen = !isOpen;
            invHud.SetActive(isOpen);
            HighlightSlot(_selectedIndex);

            // Guardamos el tiempo de esta pulsación
            lastToggleTime = Time.time;
        }
    }

    //private void HandleAddTestItem()
    //{
    //    if (addTestItemAction.ReadValue<float>() > 0.1f)
    //    {
    //        AddRandomItem();
    //    }
    //}

    private void HandleNavigation()
    {
        navTimer -= Time.deltaTime;

        Vector2 nav = navigateAction.ReadValue<Vector2>();
        if (nav == Vector2.zero)
        {
            navTimer = 0f; // reset si no hay input
            return;
        }

        if (navTimer > 0f) return;

        int delta = 0;

        if (nav.x > 0.1f) delta = 1;
        else if (nav.x < -0.1f) delta = -1;
        else if (nav.y < -0.1f) delta = columns;   // Abajo
        else if (nav.y > 0.1f) delta = -columns;  // Arriba

        // Buscamos el siguiente slot que no esté vacío
        int newIndex = _selectedIndex;
        for (int i = 0; i < _inv.Length; i++) // evitar bucle infinito
        {
            newIndex = (newIndex + delta + _inv.Length) % _inv.Length;
            if (_inv[newIndex] != null) break; // si no está vacío, usamos este
        }

        _selectedIndex = newIndex;
        HighlightSlot(_selectedIndex);

        navTimer = navCooldown;
    }

    #endregion

    #region UI

    private void HighlightSlot(int index)
    {
        for (int i = 0; i < slotImages.Length; i++)
            slotImages[i].color = (i == index) ? Color.yellow : Color.white;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < _nObj && _inv[i].icon != null)
            {
                slotImages[i].sprite = _inv[i].icon;
                slotImages[i].enabled = true;
            }
            else
            {
                slotImages[i].sprite = null;
                slotImages[i].enabled = false;
            }
        }
    }

    #endregion

    #region Inventario

    public void AddObj(InventoryItem item)
    {
        if (_nObj >= maxItems) return;

        _inv[_nObj] = item;
        _nObj++;

        item.RemoveFromWorld();
        UpdateUI();
    }

    public void RemoveObj(InventoryItem.ItemType type)
    {
        int index = -1;
        for (int i = 0; i < _nObj; i++)
        {
            if (_inv[i].GetItem() == type)
            {
                index = i;
                break;
            }
        }

        if (index == -1) return;

        for (int j = index; j < _nObj - 1; j++)
            _inv[j] = _inv[j + 1];

        _inv[_nObj - 1] = null;
        _nObj--;

        UpdateUI();
    }

    public void PickupItem(InventoryItem item)
    {
        AddObj(item);
    }

    public int CurrentObjectCount() => _nObj;

    public InventoryItem.ItemType GetItemAt(int index)
    {
        if (index >= 0 && index < _nObj)
            return _inv[index].GetItem();
        else
            return InventoryItem.ItemType.None;
    }

    //private void AddRandomItem()
    //{
    //    if (_nObj >= maxItems) return;

    //    GameObject tempGO = new GameObject("TempItem");
    //    InventoryItem tempItem = tempGO.AddComponent<InventoryItem>();
    //    tempItem.itemName = "Test Item " + (_nObj + 1);
    //    tempItem.type = (InventoryItem.ItemType)Random.Range(1, System.Enum.GetValues(typeof(InventoryItem.ItemType)).Length);

    //    AddObj(tempItem);
    //}

    #endregion
}


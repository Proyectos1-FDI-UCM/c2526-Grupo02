//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo    - JESUS DIEZ - SOBRE CODIGO DE ALEJANDRO
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
// Añadir aquí el resto de directivas using
/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>

/// <summary>
/// Inventario del jugador usando Input System directamente.
/// - Navegación toroide con teclado o gamepad
/// - Bloquea movimiento del jugador cuando está abierto el invetarui
/// - CoolDowns para desplazamientos por teclado y captura de items para el inventario
/// - Slots dinámicos con resaltado
/// - Peremite añadir items de prueba con tecla T // funcion "comentada"//Dejó el codigo anotado, 
/// pero ya no es funcional
/// </summary>
/// 


public class PlayerInventory : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

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
    
    [Header("Navegación")]
    [SerializeField] private float navigationCooldown = 0.2f; // tiempo mínimo entre movimientos
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private float lastNavTime = 0f;
    private float lastToggleTime = 0f;
    private InventoryItem[] _inv;
    private int _nObj = 0;
    private Image[] slotImages;
    private int _selectedIndex = 0;
    private bool isOpen = false;
  

    // Referencias a las acciones
    private InputAction toggleInventoryAction;
    private InputAction navigateAction;
    private InputAction addTestItemAction;
    private InputAction pickupItemAction;
    #endregion
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>

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
        //addTestItemAction = inputActions.FindAction("AddTestItem");//Se usó durante el desarrollo
        pickupItemAction = inputActions.FindAction("PickupItem");
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
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    
    //Inventario
    public bool IsOpen
    {
        get { return isOpen; }
    }
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
    #endregion
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)


    //private void HandleAddTestItem()
    //{
    //    if (addTestItemAction.ReadValue<float>() > 0.1f)
    //    {
    //        AddRandomItem();
    //    }
    //}

    private void HandleNavigation()
    {
        // Evitar que se mueva demasiado rápido
        if (Time.time - lastNavTime < navigationCooldown)
            return;

        Vector2 nav = navigateAction.ReadValue<Vector2>();
        if (nav == Vector2.zero)
            return;

        int delta = 0;

        // Determinar dirección del movimiento
        if (nav.x > 0.1f) delta = 1;       // derecha
        else if (nav.x < -0.1f) delta = -1; // izquierda
        else if (nav.y < -0.1f) delta = columns;   // abajo (una fila)
        else if (nav.y > 0.1f) delta = -columns;  // arriba (una fila)

        int nextIndex = _selectedIndex;

        // Bucle para buscar el siguiente slot ocupado
        // Esto evita seleccionar slots vacíos
        for (int i = 0; i < maxItems; i++) // límite para evitar bucles infinitos
        {
            // Calcular índice siguiente de forma toroide
            nextIndex = (nextIndex + delta + maxItems) % maxItems;

            // Si encontramos un slot con item, lo seleccionamos
            if (_inv[nextIndex] != null)
            {
                _selectedIndex = nextIndex;
                break;
            }
        }

        // Actualizar resaltado en UI
        HighlightSlot(_selectedIndex);

        // Guardar tiempo del último movimiento
        lastNavTime = Time.time;
    }


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
// class PickupItem
// namespace PlayerInventory

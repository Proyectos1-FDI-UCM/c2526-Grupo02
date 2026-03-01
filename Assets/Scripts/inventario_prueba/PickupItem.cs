using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Script para que el jugador pueda recoger items usando Input System directamente.
/// Funciona con PlayerInventory y bloquea errores de PlayerControls.
/// </summary>
public class PickupItem : MonoBehaviour
{
    
    [Header("Input System")]
    [SerializeField] private InputActionAsset inputActions; // arrastra aquí tu Input Actions Asset
    [SerializeField] private GameObject pickupIcon;

    private InputAction pickupAction;
    private InventoryItem item;
    private PlayerInventory playerInventory;
    private bool playerInRange = false;

    private void Awake()
    {
        // Buscar la acción PickupItem dentro del asset
        if (inputActions != null)
            pickupAction = inputActions.FindAction("PickupItem");
    }

    private void OnEnable()
    {
        pickupAction?.Enable();
    }

    private void OnDisable()
    {
        pickupAction?.Disable();
    }

    private void Start()
    {
        item = GetComponent<InventoryItem>();
    }

    private void Update()
    {
        if (!playerInRange || pickupAction == null)
            return;

        // Leer valor de la acción "PickupItem"
        float pickupValue = pickupAction.ReadValue<float>();
        if (pickupValue > 0.1f)
        {
            if (playerInventory != null)
            {
                playerInventory.PickupItem(item);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<PlayerInventory>();
            playerInRange = playerInventory != null;

            if (pickupIcon != null)
                pickupIcon.SetActive(true); // mostrar icono al acercarse
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = null;
            playerInRange = false;

            if (pickupIcon != null)
                pickupIcon.SetActive(false); // ocultar icono al alejarse
        }
    }
}

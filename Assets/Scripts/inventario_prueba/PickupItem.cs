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

    private void Start()
    {
        if (inputActions != null)
        {
            pickupAction = inputActions.FindAction("Pickup");
            Debug.Log($"{gameObject.name} Start: acción PickupItem encontrada: {pickupAction != null}");
        }

        item = GetComponent<InventoryItem>();
        Debug.Log($"{gameObject.name} Start: InventoryItem encontrado: {item != null}");
    }


    private void Update()
    {
        if (!playerInRange)
        {
            return;
        }

        if (pickupAction == null)
        {
            Debug.Log($"{gameObject.name} Update: pickupAction es null");
            return;
        }

        float pickupValue = pickupAction.ReadValue<float>();
        if (pickupValue > 0.1f)
        {
            if (playerInventory != null)
            {
                //Debug.Log($"{gameObject.name} Update: Pulsada E, intentando recoger objeto...");
                playerInventory.PickupItem(item);
                //Debug.Log($"{gameObject.name} Update: Objeto recogido por {playerInventory.gameObject.name}");

                // Desactivar el icono después de recoger
                if (pickupIcon != null)
                    pickupIcon.SetActive(false);

                // Desactivar el objeto en la escena
                gameObject.SetActive(false);
            }
            else
            {
                //Debug.Log($"{gameObject.name} Update: playerInventory es null, no se puede recoger");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player_Controller player = other.GetComponent<Player_Controller>();
        if (player != null)
        {
            playerInventory = player.GetComponent<PlayerInventory>();
            playerInRange = playerInventory != null;

            if (pickupIcon != null)
                pickupIcon.SetActive(true);

            //Debug.Log($"{gameObject.name} TriggerEnter: jugador detectado, playerInRange={playerInRange}, inventory={playerInventory != null}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player_Controller player = other.GetComponent<Player_Controller>();
        if (player != null)
        {
            playerInventory = null;
            playerInRange = false;

            if (pickupIcon != null)
                pickupIcon.SetActive(false);

            //Debug.Log($"{gameObject.name} TriggerExit: jugador salió, playerInRange={playerInRange}");
        }
    }
}


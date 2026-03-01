//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo  -JESUS DIEZ-
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------
using UnityEngine;
using UnityEngine.InputSystem;
// Añadir aquí el resto de directivas using
/// <summary>
/// Script para que el jugador pueda recoger items usando Input System directamente.
/// Funciona con PlayerInventory
/// </summary>
/// 


public class PickupItem : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [Header("Input System")]
    [SerializeField] private InputActionAsset inputActions; // arrastra aquí tu Input Actions Asset
    [SerializeField] private GameObject pickupIcon;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

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
    /// 
    private void Start()
    {
        item = GetComponent<InventoryItem>();

        // Asegurarse de que el icono está apagado al iniciar
        if (pickupIcon != null)
            pickupIcon.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player_Controller player = other.GetComponent<Player_Controller>();
        if (player != null)
        {
            playerInventory = player.GetComponent<PlayerInventory>();
            playerInRange = playerInventory != null;

            if (pickupIcon != null)
                pickupIcon.SetActive(true); // mostrar icono al acercarse
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
                pickupIcon.SetActive(false); // ocultar icono al alejarse
        }
    }
    #endregion
    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class PickupItem 
// namespace



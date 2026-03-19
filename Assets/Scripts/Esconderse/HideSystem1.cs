//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo JESUS
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class HideSystem1 : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [Header("Configuración de escondite")]
    // Asset de Input System (.inputactions)
    [SerializeField] private InputActionAsset inputActions;
    // Nombre de la acción dentro del asset
    [SerializeField] private string hideActionName = "Hide";
    // Tiempo mínimo entre inputs para evitar spam
    [SerializeField] private float inputCooldown = 0.3f;     
    
    //UI que indica "Presiona E/A para esconderte"
    [Header("UI")]
    [SerializeField] private GameObject hidePromptUI;     

    //Referencia al controlador del moviento
    [Header("UI")]
    [SerializeField] private Player_Controller playerController; 
    

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    
    private bool isHiding = false;
    // Referencia al objeto interactuable donde se puede esconder
    private Interactuable currentHideSpot;
    // Guarda el tiempo del último input para cooldown
    private float lastInputTime;
    // Acción Hide del InputActionAsset
    private InputAction _hideAction;                         


    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start se llama al iniciar el script.
    /// Inicializa la acción Hide del InputActionAsset y activa la UI si es necesario.
    /// </summary>
    void Start()
    {
        // Busca la acción Hide dentro del InputActionAsset usando su nombre
        _hideAction = inputActions.FindAction(hideActionName);

        // Comprobar si se encontró la acción
        if (_hideAction == null)
        {
            Debug.LogError("No se encontró la acción Hide en el Input Action Asset");
        }
        else
        {
            _hideAction.Enable(); // Habilita la acción para que pueda recibir input
        }

        // Ocultar la UI de interacción al inicio
        if (hidePromptUI != null)
            hidePromptUI.SetActive(false);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    /// <summary>
    // Update se llama una vez por frame.
    // Comprueba si el jugador presiona la acción Hide para alternar escondido.
    /// </summary>
    void Update()
    {
        HandleHidingInput();
    }
    /// <summary>
    // Detecta cuando el jugador entra en el rango de un Interactuable.
    // Activa la UI de interacción.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        Interactuable interact = other.GetComponent<Interactuable>();
        if (interact != null)
        {
            currentHideSpot = interact;

            // Mostrar UI si no está escondido
            if (!isHiding && hidePromptUI != null)
                hidePromptUI.SetActive(true);
        }
    }

    /// <summary>
    // Detecta cuando el jugador sale del rango de un Interactuable.
    // Desactiva la UI y limpia la referencia al escondite.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        Interactuable interact = other.GetComponent<Interactuable>();
        if (interact != null && interact == currentHideSpot)
        {
            // Ocultar UI si no está escondido
            if (!isHiding && hidePromptUI != null)
                hidePromptUI.SetActive(false);

            currentHideSpot = null; // Limpiar referencia
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
    /// <summary>
    // Propiedad pública de solo lectura para saber si el jugador está escondido.
    /// </summary>
    public bool IsHiding
    {
        get { return isHiding; }
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    /// <summary>
    // Comprueba el input de Hide y activa el escondite si corresponde.
    // Incluye cooldown para evitar spam.
    /// </summary>
    private void HandleHidingInput()
    {
        // Si la acción no existe, salir
        if (_hideAction == null) return;

        // Si el cooldown aún no ha pasado, salir
        if (Time.time - lastInputTime < inputCooldown) return;

        // Si no hay escondite en rango, salir
        if (currentHideSpot == null) return;

        // Si la acción Hide se presionó este frame
        if (_hideAction.WasPressedThisFrame())
        {
            ToggleHiding();                  // Alterna el estado de escondido
            lastInputTime = Time.time;       // Actualiza tiempo del último input
        }
    }

    /// <summary>
    // Alterna el estado de escondido del jugador.
    // Mueve al jugador al escondite, oculta o muestra render y collider, y actualiza la UI.
    /// </summary>
    private void ToggleHiding()
    {
        isHiding = !isHiding;

        if (isHiding)
        {
            // Parar movimiento del jugador
            if (playerController != null)
                playerController.Stop();

            // Colocar jugador en el escondite
            transform.position = currentHideSpot.transform.position;

            // Ocultar jugador
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            if (hidePromptUI != null)
                hidePromptUI.SetActive(false);
        }
        else
        {
            // Reanudar movimiento del jugador
            if (playerController != null)
                playerController.Resume();

            // Mostrar jugador
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider>().enabled = true;

            if (hidePromptUI != null)
                hidePromptUI.SetActive(true);
        }
    }


    #endregion

} // class NewMonoBehaviourScript 
// namespace

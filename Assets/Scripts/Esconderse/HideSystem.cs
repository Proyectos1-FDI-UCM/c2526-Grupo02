//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
// Sistema encargado de gestionar el estado de escondite del jugador.
// Permite entrar y salir de un escondite, controlando visibilidad,
// colisiones y evitando spam de input.

public class HideSystem : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [Header("Configuración")]


    //Permite acceder al Player_Controler para para el jugador cuando está escondido
    [SerializeField] private Player_Controller movement;

   

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    //Indica si el jugador está actualmente escondido
    private bool _isHiding = false;
    //Guarda el último momento en el que se interactuó
    private float _lastInputTime;
  
    // Referencia al Renderer del jugador
    private Renderer _renderer;

    // Referencia al Collider del jugador
    private Collider2D _collider;


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
    /// <summary>
    /// Inicializa referencias necesarias
    /// </summary>
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider2D>();

        // Programación defensiva
        if (_renderer == null)
            Debug.LogWarning("No se encontró Renderer en el jugador");

        if (_collider == null)
            Debug.LogWarning("No se encontró Collider en el jugador");
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
    /// Alterna el estado de escondite del jugador.
    /// Se llama desde un HideSpot al interactuar.
    /// </summary>
    /// <param name="hideSpot">Transform del punto donde esconderse</param>
    public void ToggleHide()
    {
        // Evita spam de interacción
        //if (Time.time - _lastInputTime < inputCooldown)
        //    return;

        _isHiding = !_isHiding;
        //_lastInputTime = Time.time;

        if (_isHiding)
        {
            EnterHide();
        }
        else
        {
            ExitHide();
        }
    }
    //permite acceder a la variable privad isHIding a traves de propiedad
    public bool IsHiding
    {
        get { return _isHiding; }
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    /// <summary>
    /// Ejecuta la lógica al entrar en un escondite
    /// </summary>
    private void EnterHide()
    {
        // Mover al jugador al punto de escondite
        

        // Ocultar visualmente al jugador
        if (_renderer != null)
        {
            _renderer.enabled = false;

            movement?.Stop(); // Detiene al jugador
        }

        // Desactivar colisiones para evitar detección
        //if (_collider != null)
        //_collider.enabled = false;
    }

    /// <summary>
    // Ejecuta la lógica al salir del escondite
    /// </summary>
    private void ExitHide()
    {
        // Mostrar jugador
        if (_renderer != null)
        {
            _renderer.enabled = true;
            movement?.Resume(); // Vuelve a permitir el movimiento
        }
            // Reactivar colisiones
                            //if (_collider != null)
                            //_collider.enabled = true;
    }
    #endregion

} // class HideSystem 
// namespace

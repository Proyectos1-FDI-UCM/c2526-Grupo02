//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo JESUS DIEZ
// Nombre del juego - Dont go up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


// Se encarga de gestionar el estado de escondite del jugador.
// Permite entrar y salir de un escondite, controlando visibilidad,
// colisiones

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

    // Inicializa referencias necesarias
    
    private void Start()
    {
        //Referencia al sprite
        _renderer = GetComponent<Renderer>();
        //Referencia al collider
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
    

    //Activa y desactiva si el jugdor esta escondido
    public void ToggleHide()
    {
        _isHiding = !_isHiding;

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

    // Ejecuta la lógica al entrar en un escondite
    private void EnterHide()
    {
        // Ocultar visualmente al jugador
        if (_renderer != null)
        {
            _renderer.enabled = false;
        
            // Detiene al jugador
            movement?.Stop(); 
        }
    }

    // Ejecuta la lógica al salir del escondite
    private void ExitHide()
    {
        // Mostrar jugador
        if (_renderer != null)
        {
            _renderer.enabled = true;
            movement?.Resume(); // Vuelve a permitir el movimiento
        }
    }
    #endregion

} // class HideSystem 
// namespace

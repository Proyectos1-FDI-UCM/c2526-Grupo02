//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo JESUS DIEZ
// Nombre del juego - Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
/// <summary>
/// Gestiona el sistema de escondite del jugador.
/// Permite entrar y salir de escondites, controlando la visibilidad,
/// el movimiento y el estado del personaje.
/// <summary>

public class HideSystem : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [Header("Configuración")]

    //Permite acceder al Player_Controler para para el jugador cuando está escondido
    [SerializeField] private Player_Controller movement;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
   
    //Indica si el jugador está actualmente escondido
    private bool _isHiding = false;

    // Referencia al Renderer del jugador
    private Renderer _renderer;

    // Referencia al Collider del jugador
    private Collider2D _collider;
   
    private float _counter = 0;
    private float _maxCounter = 0.001f;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void Update()
    {
       
        if(_counter > 0)
        { 
        _counter -= Time.deltaTime;
        }
    }
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
    /// <summary>
    /// Alterna entre entrar y salir del estado de escondite.
    /// Solo permite el cambio si ha pasado el tiempo de espera (_counter),
    /// evitando pulsaciones rápidas consecutivas.
    /// </summary>
    public void ToggleHide()
    {
        // Comprueba que el cooldown haya finalizado
        if (_counter <= 0)
        {
            if (!_isHiding)
            {
        // Si el jugador NO está escondido, entra en modo escondite
                _isHiding = !_isHiding;
        // Ejecuta la lógica de ocultarse (desactivar visibilidad, movimiento, etc.)
                EnterHide();

           // Reinicia el cooldown
                _counter = _maxCounter;
            }
            else
            {
            // Cambia el estado a visible
                _isHiding = !_isHiding;
            ExitHide();
            // Reinicia el cooldown
                _counter = _maxCounter;
            }
        }
    
    }
    
    //permite acceder a la variable privada isHiding a traves de propiedad
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
            movement.Stop(); 
        }
    }

    // Ejecuta la lógica al salir del escondite
    private void ExitHide()
    {
        // Mostrar jugador
        if (_renderer != null)
        {
            _renderer.enabled = true;
            movement.Resume(); // Vuelve a permitir el movimiento
        }
    }
    #endregion

} // class HideSystem 
// namespace

//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Este archivo sirve para contener el comportamiengto del enemigo armario
// Responsable de la creación de este archivo - Sara Quilez Martinez
// Don't look up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Armario : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    private float timeToDeactivate; // Tiempo que tarda el armario en apagar su campo de visión
    [SerializeField]
    private float deactivationDuration; // Tiempo que tarda en volver a activarlo 
    [SerializeField]
    private GameObject visualPanel; // Panel que señaliza la visión del enemigo
    [SerializeField]
    private Collider2D visionCollider; //Collider aparte que representa el campo visual del enemigo.



    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private float _timer;  // _timer que se usara para llevar los dos tiempos.
    private Animator _animator;
    enum CabinetState  // enum para indicar en que _currentState se encuentra el armario
    {
        Active,
        Inactive
    }
    private CabinetState _currentState;  // el _currentState sirve para el estado en el que nos encontramos

    private void SetVisualPanelActive (bool Acti) // Controlo el panel que me indica el radio visual del enemigo
    {
        visualPanel.SetActive(Acti); 
       
    }
    private void UpdateVisuals(bool active)
    {
        visualPanel.SetActive(active);
        visionCollider.enabled = active;

        if (_animator != null)
        {
            _animator.SetBool("Open", active);
        }
    }
    private void SetVisionActive (bool Acti) // Controlo el propio collider para activarlo y desactivarlo
    {
        visionCollider.enabled = Acti;
    }
    private Enemy_Detect _enemyDetect;  // llamo al _enemyDetect que tiene el propio enemigo
    private Phases Phase;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Awake()
    {
        _animator = GetComponent<Animator>();
        Phase = GetComponent<Phases>();
        if (Phase == null)
        {
            Debug.Log("No esta el script phase en el enemigo");
        }
        Phase.SetVisualPanel(visualPanel);
        _currentState = CabinetState.Active;
        _enemyDetect = visualPanel.GetComponent<Enemy_Detect>();
        if (_enemyDetect == null)
        {
            Debug.Log("No hay enemyDetect en el panelvisual");
        }
        if (_animator == null)
        {
            Debug.LogError("No se encontró Animator en el Armario.");
        }
        UpdateVisuals(true);



    }
    private void OnTriggerStay2D(Collider2D collision) // Mientras el jugador este dentro del enemigo se irá comprobando en que fase se encuentra
    {
        Phase.EnemyPhases(collision);
    }
    private void ChangeState(CabinetState newState)
    {
        _currentState = newState;
        _timer = 0;
        UpdateVisuals(_currentState == CabinetState.Active);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!Pausa_controller.IsGamePaused)
        {
            _timer += Time.deltaTime;
        }
        if (_currentState == CabinetState.Active && _timer >= timeToDeactivate)
            {
                _currentState = CabinetState.Inactive;
                _timer = 0;
                UpdateVisuals(false); 
            }
            else if (_currentState == CabinetState.Inactive && _timer >= deactivationDuration)
            {
                _currentState = CabinetState.Active;
                _timer = 0;
                UpdateVisuals(true);
            }
        
    }


    #endregion
}

 // class Armario 
// namespace

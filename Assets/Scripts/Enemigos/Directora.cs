//---------------------------------------------------------
// Lógica detrás del funcionamiento de la Directora
// Responsable de la creación de este archivo - Pablo
// Don´t Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;


/// <summary>
/// Es la lógica que hace que la Directora funcione de la manera esperada.
/// </summary>
public class Directora : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    private float _changeTime; //Tiempo que tarda en cambiar de mirar un lado a otro
    [SerializeField]
    private float _lookingTime; //Tiempo que mira en una dirección
    [SerializeField]
    private Vector3[] _positions; //Array de coordenadas que mira
    [SerializeField]
    private GameObject _visualPanel; //Panel que señaliza la visión del enemigo


    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private float _temporizador;  // Temporizador que se usara para llevar los dos tiempos.
    // enum para indicar en que estado se encuentra la Directora
    enum Estado {activo, inactivo}
    private Estado _estado;  // el estado
    private int i = 0;
    private Phases Phase;
    private Animator _animator;

    private void ControlPanel(bool Acti) // Controlo el panel que me indica el radio visual del enemigo
    {
        _visualPanel.SetActive(Acti);
    }

    private Enemy_Detect _detect;  // llamo al detect que tiene que estar dentro de la Directora
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Awake()
    {
        Phase = GetComponent<Phases>();
        if (Phase == null)
        {
            Debug.Log("No esta el script phase en el enemigo");
        }
        Phase.SetVisualPanel(_visualPanel);
        _estado = Estado.activo;

        _detect = _visualPanel.GetComponent<Enemy_Detect>(); // llamo a los componentes que me harán falta más adelante
        if (_detect == null)
        {
            Destroy(this);
        }
        if(_positions.Length <= 1) //Revisa que haya más de una coordenada donde cambiar
        {
            UnityEngine.Debug.Log("Tiene que haber como mínimo 2 coordenadas entre las que alternar");
            Destroy(this);
        }
        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.Log("No hay animator en el enemigo");
        }
    }

    private void ControlVision(int i) //Cambia progresivamente las coordenadas en el patrón deseado
    {
        _visualPanel.transform.localPosition = _positions[i];
    }
    private void OnTriggerStay2D(Collider2D collision) // Mientras el jugador este dentro del enemigo se irá comprobando en que fase se encuentra
    {
        Phase.EnemyPhases(collision);
    }


    void Update()
    {
        _temporizador += Time.deltaTime;
        if (_estado == Estado.activo && _temporizador >= _lookingTime && !Pausa_controller.IsGamePaused)
        {
            _estado = Estado.inactivo;
            _temporizador = 0;
            ControlPanel(false);
            ControlVision(i);
            _animator.SetInteger("State", i);
            i++;
        }
        else if (_estado == Estado.inactivo && _temporizador >= _changeTime && !Pausa_controller.IsGamePaused)
        {
            _estado = Estado.activo;
            _temporizador = 0;
            ControlPanel(true);
            
        }
        if (i == _positions.Length) i = i - i;
       
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion   

} // class Directora 
// namespace

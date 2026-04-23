//---------------------------------------------------------
// Lógica detrás del funcionamiento de la Lámpara
// Responsable de la creación de este archivo - Pablo
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using static UnityEngine.GraphicsBuffer;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Lamp : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    private float _lookingTime; //Tiempo que mira en una dirección
    [SerializeField]
    private Vector3[] _limits; //Array de coordenadas que se para
    [SerializeField]
    private GameObject _visualPanel; //Panel que señaliza la visión del enemigo
    [SerializeField]
    private float SpringFactor = 5f; // Velocidad de suavizado del movimiento del enemigo

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private float _temporizador;  // Temporizador que se usara para llevar los dos tiempos.
    // enum para indicar en que estado se encuentra la Lámpara
    enum Estado { activo, inactivo }
    private Estado _estado;  // el estado
    private int i = 0;
    private Phases Phase;
    private Transform Target;

    private Enemy_Detect _detect;  // llamo al detect que tiene que estar dentro de la Lámpara

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Start()
    {
        _visualPanel.SetActive(true);
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
    }

    private void ControlVision(int i) //Cambia progresivamente las coordenadas en el patrón deseado
    {
        _visualPanel.transform.localPosition = _limits[i];
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
            ControlVision(i);
            i++;
        }
        else if (_estado == Estado.inactivo && !Pausa_controller.IsGamePaused)
        {
            _estado = Estado.activo;
            _temporizador = 0;
        }
        if (!Pausa_controller.IsGamePaused)
        {
            // Posición actual de la coordenada
            Vector3 tarPos = Target.transform.position;

            // Posición actual de la lámpara
            Vector3 pos = transform.position;

            // Si no hay colisiones, la cámara sigue al jugador suavemente
            if (tarPos != pos)
            {
                pos.x = Mathf.Lerp(pos.x, tarPos.x, (SpringFactor * Time.deltaTime));
                transform.position = pos;
            }
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion   

} // class Lampara 
// namespace

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
    private bool inv = false;
    private bool _come = false; // true cuando el panel ya alcanzó el punto actual

    private Phases _phase;
    private Enemy_Detect _detect;  // llamo al detect que tiene que estar dentro de la Lámpara

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Awake()
    {
        _visualPanel.SetActive(true);
        _phase = GetComponent<Phases>();
        if (_phase == null)
        {
            Debug.Log("No esta el script phase en el enemigo");
        }
        else _phase.SetVisualPanel(_visualPanel);

        _detect = _visualPanel.GetComponent<Enemy_Detect>(); // llamo a los componentes que me harán falta más adelante
        if (_detect == null)
        {
            Destroy(this);
        }

        _estado = Estado.activo;
    }

    void Update()
    {
        if (Pausa_controller.IsGamePaused) return;
        if (_limits == null || _limits.Length == 0) return;

        switch (_estado)
        {
            case Estado.activo:
                MoverPanel();
                break;

            case Estado.inactivo:
                _temporizador += Time.deltaTime;
                if (_temporizador >= _lookingTime)
                {
                    _temporizador = 0f;
                    _come = false;
                    AvanzarIndice();
                    _estado = Estado.activo;
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _phase.EnemyPhases(collision);
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    private void MoverPanel()
    {
        Vector3 destino = _limits[i];
        Vector3 posActual = _visualPanel.transform.localPosition;

        // Mueve el panel suavemente hacia el punto actual del array
        _visualPanel.transform.localPosition = Vector3.Lerp(
            posActual,
            destino,
            SpringFactor * Time.deltaTime
        );

        // Comprueba si llegó suficientemente cerca
        if (!_come && Vector3.Distance(posActual, destino) < 0.05f)
        {
            _visualPanel.transform.localPosition = destino; // Cuadra el panel en la posición
            _come = true;
            _estado = Estado.inactivo;
            _temporizador = 0f;
        }
    }

    private void AvanzarIndice()
    {
        if (!inv)
        {
            if (i < _limits.Length - 1)
                i++;
            else
            {
                inv = true;
                i--;
            }
        }
        else
        {
            if (i > 0)
                i--;
            else
            {
                inv = false;
                i++;
            }
        }
    }

    #endregion

} // class Lampara 
// namespace

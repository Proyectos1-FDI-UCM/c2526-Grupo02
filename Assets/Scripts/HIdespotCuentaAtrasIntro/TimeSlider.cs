//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo JESUS DIEZ
// Nombre del juego - Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
// Librería para usar textos de TextMeshPro
using TMPro;
// Necesario para cambiar de escena
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// Sistema de cronómetro con cuenta atrás.
/// Muestra el tiempo restante mediante un slider y texto UI.
/// Puede iniciarse manualmente o desde el Inspector.
/// Al llegar a cero detiene al jugador y ejecuta un evento de final de partida.
/// </summary>
public class TimeSlider : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    
    [Header("Activación/desactivación desde Inspector")]
    [SerializeField]
    private bool activateTimerInInspector = false;

    // Canvas que contiene la UI del cronómetro (slider y texto)
    [SerializeField] private GameObject timerCanvas;
    
    //Referencia al slider del HUB
    [SerializeField] private Slider slider;

    // Tiempo total que durará el cronómetro (en segundos)
    [SerializeField] private float duration = 10f;
    
    // Objeto o UI de Game Over (final de la partida)
    [SerializeField] private GameObject GameOver;

    // Texto que muestra el tiempo restante en pantalla
    [SerializeField] private TMP_Text timerText;

    // Evento ejecutado cuando el tiempo llega a cero (Game Over, cambio de escena, etc.).
    // Se ejecuta cuando el temporizador llega a cero.
    // Permite enlazar acciones como cambiar de escena, mostrar UI o activar GameOver
    // sin necesidad de escribir código adicional.
    [SerializeField]
    UnityEvent code;

    // Referencia al jugador para detener su movimiento al finalizar el tiempo
    [SerializeField] private Player_Controller player;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    // Tiempo acumulado desde que empezó el cronómetro
    private float _timePassed = 0f;

    // Indica si el cronómetro está activo
    private bool _isRunning = false;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    //los paneles se ponen inactivos al arrancar la escena
    void Start()
    {
        // Oculta la UI del cronómetro al iniciar la escena
        if (timerCanvas != null)
            timerCanvas.SetActive(false);

        // No empieza solo, espera StartTimer()
        _isRunning = false; 
    }

    void Update()
    {
        // No actualiza el cronómetro si el juego está pausado o el jugador ha muerto
        if (!Pausa_controller.IsGamePaused && !LevelManager.Instance.GetDeath())
        {
            // Permite iniciar automáticamente el cronómetro desde el Inspector
            if (activateTimerInInspector)
            {
                if (!_isRunning) StartTimer();
            }

            //Lógica del temporizador
            // Actualiza el cronómetro solo si está en ejecución
            // Si el temporizador está detenido, no ejecuta nada
            if (!_isRunning) return;

            // Suma el tiempo que ha pasado desde el último frame
            // (esto hace que funcione correctamente sin importar los FPS)
            _timePassed += Time.deltaTime;

            // Calcula cuánto tiempo queda
            float timeLeft = duration - _timePassed;

            // Normaliza el tiempo entre 0 y 1 para el slider
            slider.value = _timePassed / duration;

            // Actualiza el texto mostrando el tiempo restante
            // Muestra el tiempo restante redondeado
            timerText.text = Mathf.Round(timeLeft) + "s";

            // Si el tiempo llega a cero, finaliza la partida
            if (_timePassed >= duration)
            {
                // Detiene el temporizador
                _isRunning = false;

                // Cambia el texto final
                timerText.text = "¡Demasiado tarde...!";

                // Para al jugador
                if (player != null)
                {
                    player.Stop();
                }

                // Llama a la función del final malo
                BadEnding();
            }
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Ejecuta el evento de final de partida (Game Over)
    public void BadEnding()
    {
        code.Invoke();
    }

    // Inicia el cronómetro y muestra la UI correspondiente
    public void StartTimer()
    {
        // Activa el cronómetro
        _isRunning = true;
        // Reinicia el tiempo transcurrido
        _timePassed = 0f;

        // Muestra la interfaz del cronómetro
        if (timerCanvas != null)
            timerCanvas.SetActive(true);

        // Oculta el panel si está visible

    }
    //Detiene el temporizador y oculta el panel con el cronómetro
    public void StopTimer()
    {
        _isRunning = false;

        // Oculta el canvas del temporizador
        if (timerCanvas != null)
            timerCanvas.SetActive(false);
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados


    #endregion

} // class TimeSlider 
// namespace

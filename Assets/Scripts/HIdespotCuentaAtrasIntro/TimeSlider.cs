//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo JESUS DIEZ
// Nombre del juego - Dont go up
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
//Cronómetro, cuenta a tras configurable
//en el editor, se muestra en el HUB atraves de un collader
//y un texto que va marcha atra.
//Tiene un metodo públic Star iniciar el cronometro
//Tiene un método públic Stop para parar el cronómetro
//y ocultar los paneles.
//Cuando termina de contar el cronómetro para al jugador.
//El cronómetro puede activarse desde el inspector
//Si el cronómetro alcanza el cero lleva 
//al final malo "BadEnding()" que bien
//(activa el panel)  o escena (codigo anotado) de "badending".

/// </summary>
public class TimeSlider : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    
    [Header("Activacion/desactivacion desde Inspector")]
    [SerializeField]
    private bool activateTimerInInspector = false;


    // Canvas que contiene slider y text
    [SerializeField] private GameObject timerCanvas;
    
    //Referencia al slider del HUB
    [SerializeField] private Slider slider;

    // Tiempo total que durará el cronómetro (en segundos)
    [SerializeField] private float duration = 10f;
    [SerializeField]
        private GameObject GameOver;

    // Referencia al texto que mostrará el tiempo en pantalla
    [SerializeField] private TMP_Text timerText;

    // Nombre de la escena de final malo
    //[SerializeField] private string badEndingSceneName = "BadEnding";

    [SerializeField]
    UnityEvent code;

    // Referencia al jugador, para llamar a su funcion "stop" para poder pararlo
    [SerializeField] private Player_Controller player;  

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    
    // Tiempo que ha pasado desde que empezó el contador
    private float _timePassed = 0f;

    // Controla si el temporizador está activo
    private bool _isRunning = false;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    //los paneles se ponen inactivos al arrancar la escena
    void Start()
    {
        //panel del slider y cronometro apagados al iniciar la escena
        if (timerCanvas != null)
            timerCanvas.SetActive(false);

        //Panel del final malo apagado al iniciar la escena

        // No empieza solo, espera StartTimer()
        _isRunning = false; 
    }

    void Update()
    {
        if (!Pausa_controller.IsGamePaused && !LevelManager.Instance.GetDeath())
        {
            /// Control desde Inspector
            if (activateTimerInInspector)
            {
                if (!_isRunning) StartTimer();
            }

            ///Lógica del temporizador

            // Si el temporizador está detenido, no ejecuta nada
            if (!_isRunning) return;

            // Suma el tiempo que ha pasado desde el último frame
            // (esto hace que funcione correctamente sin importar los FPS)
            _timePassed += Time.deltaTime;

            // Calcula cuánto tiempo queda
            float timeLeft = duration - _timePassed;

            // Actualiza el slider (valor entre 0 y 1)
            slider.value = _timePassed / duration;

            // Actualiza el texto mostrando el tiempo restante
            //  Redondea hacia arriba (ej: 2.3 -> 3)
            timerText.text = Mathf.Round(timeLeft) + "s";

            // Si el tiempo se ha terminado...
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
   //Lleva al final malo
    public void BadEnding()
    {
        code.Invoke();
    }

    // Función para iniciar el temporizador 
    public void StartTimer()
    {
        //arranca el timmer
        _isRunning = true;
        //lo pone a cero
        _timePassed = 0f;

        //Hace visble canvas donde va el contador y el slider
        if (timerCanvas != null)
            timerCanvas.SetActive(true);

        // Oculta el panel si está visible

    }
    //Detiene el timer y oculta el panel con el cronómetro
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

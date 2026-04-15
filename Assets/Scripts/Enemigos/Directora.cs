//---------------------------------------------------------
// Lógica detrás del funcionamiento de la Directora
// Responsable de la creación de este archivo - Pablo
// Nombre del juego
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
    private float TiempoDeCambio; //Tiempo que tarda en cambiar de mirar un lado a otro
    [SerializeField]
    private float TiempoQueMira; //Tiempo que mira en una dirección
    [SerializeField]
    private Vector3[] Posiciones; //Array de coordenadas que mira
    [SerializeField]
    private GameObject PanelVisual; //Panel que señaliza la visión del enemigo


    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private float Temporizador;  // Temporizador que se usara para llevar los dos tiempos.
    // enum para indicar en que estado se encuentra el armario
    enum Estado {activo, inactivo}
    private Estado estado;  // el estado
    private int i = 0;

    private void ControlPanel(bool Acti) // Controlo el panel que me indica el radio visual del enemigo
    {
        PanelVisual.SetActive(Acti);
    }


    private Enemy_Detect detect;  // llamo al detect que tiene que estar dentro del armario
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Start()
    {
        estado = Estado.activo;

        detect = PanelVisual.GetComponent<Enemy_Detect>(); // llamo a los componentes que me harán falta más adelante
        if (detect == null)
        {
            Destroy(this);
        }
        if(Posiciones.Length <= 1) //Revisa que haya más de una coordenada donde cambiar
        {
            UnityEngine.Debug.Log("Tiene que haber como mínimo 2 coordenadas entre los que alternar");
            Destroy(this);
        }
     
    }

    private void ControlVision(int i) //Cambia progresivamente las coordenadas en el patrón deseado
    {
        PanelVisual.transform.localPosition = Posiciones[i];
    }
    private void OnTriggerStay2D(Collider2D collision) // Mientras el jugador este dentro del enemigo se irá comprobando en que fase se encuentra
    {
        var player = collision.GetComponent<Player_Controller>();


        if (player != null && !Pausa_controller.IsGamePaused)
        {
            int fase = detect.GetState();
            switch (fase)
            {
                case 0:
                    this.GetComponent<SpriteRenderer>().color = Color.white;
                    break;
                case 1: case 2:
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                    break;
                case 3:
                    GameManager.Instance.GameOver();
                    break;
            }
        }
    }


    void Update()
    {
        Temporizador += Time.deltaTime;
        if (estado == Estado.activo && Temporizador >= TiempoQueMira && !Pausa_controller.IsGamePaused)
        {
            estado = Estado.inactivo;
            Temporizador = 0;
            ControlPanel(false);
            ControlVision(i);
            i++;
        }
        else if (estado == Estado.inactivo && Temporizador >= TiempoDeCambio && !Pausa_controller.IsGamePaused)
        {
            estado = Estado.activo;
            Temporizador = 0;
            ControlPanel(true);
        }
        if (i == Posiciones.Length) i = i - i;
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

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
public class Directora : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    private float TiempoDeCambio; //Tiempo que tarda en cambiar de mirar un lado a otro
    [SerializeField]
    private float TiempoQueMira; //Tiempo que mira en una dirección
    [SerializeField]
    private Vector3[] Posiciones; //Array de coordenadas que mira
    [SerializeField]
    private GameObject PanelVisual; // Panel que señaliza la visión del enemigo


    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private float Temporizador;  // Temporizador que se usara para llevar los dos tiempos.
    // enum para indicar en que estado se encuentra el armario
    enum Estado {activo, inactivo}
    private Estado estado;  // el estado

    private void ControlPanel(bool Acti) // Controlo el panel que me indica el radio visual del enemigo
    {
        PanelVisual.SetActive(Acti);
    }


    private Enemy_Detect detect;  // llamo al detect que tiene que estar dentro del armario
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
    void Start()
    {
        estado = Estado.activo;

        detect = GetComponent<Enemy_Detect>(); // llamo a los componentes que me harán falta más adelante
        if (detect == null)
        {
            Destroy(this);
        }
        if(Posiciones.Length <= 1) //Por ahora puedes tener colliders vacios, MAL AHÍ
        {
            UnityEngine.Debug.Log("Tiene que haber como mínimo 2 colliders entre los que alternar");
            Destroy(this);
        }
    }

    private void ControlVision(int i) //Desactiva y activa progresivamente los colliders en el patrón deseado
    {
        PanelVisual.transform.position = Posiciones[i];
    }
    private void OnTriggerStay2D(Collider2D collision) // Mientras el jugador este dentro del enemigo se irá comprobando en que fase se encuentra
    {
        var player = collision.GetComponent<Player_Controller>();


        if (player != null)
        {
            int fase = detect.GetState();
            switch (fase)
            {
                case 0:
                    GetComponent<Renderer>().material.color = Color.white;
                    break;
                case 1:
                    GetComponent<Renderer>().material.color = Color.red;
                    break;
                case 2:
                    GameManager.Instance.GameOver();
                    break;
            }
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Temporizador += Time.deltaTime;
        int i = 0;
        if (estado == Estado.activo && Temporizador >= TiempoQueMira)
        {
            estado = Estado.inactivo;
            Temporizador = 0;
            ControlPanel(false);
            ControlVision(i);
            i++;
        }
        else if (estado == Estado.inactivo && Temporizador >= TiempoDeCambio)
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
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class Directora 
// namespace

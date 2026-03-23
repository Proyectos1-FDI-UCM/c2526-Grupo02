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
    private float TiempoDeDesactivar; // Tiempo que tarda el armario en apagar su campo de visión
    [SerializeField]
    private float DuracionDeDesactivacion; // Tiempo que tarda en volver a activarlo 
    [SerializeField]
    private GameObject PanelVisual; // Panel que señaliza la visión del enemigo
    [SerializeField]
    private Collider2D Visión; //Collider aparte que representa el campo visual del enemigo.



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

    enum Estado  // enum para indicar en que estado se encuentra el armario
    {
        activo,
        inactivo
    }
    private Estado estado;  // el estado

    private void ControlPanel (bool Acti) // Controlo el panel que me indica el radio visual del enemigo
    {
        PanelVisual.SetActive(Acti); 
       
    }

    private void ControlVision (bool Acti) // Controlo el propio collider para activarlo y desactivarlo
    {
        Visión.enabled = Acti;
    }
    private Enemy_Detect detect;  // llamo al detect
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
        detect = GetComponent<Enemy_Detect>();

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player_Controller>();


        if (player != null)
        {
            int fase = detect.GetState();
            switch (fase)
            {
                case 0:
                    GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case 1:
                    GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case 2:
                    //GetComponent<Renderer>().material.color = Color.red;
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
        if (estado == Estado.activo && Temporizador >= TiempoDeDesactivar)
        {
            estado = Estado.inactivo;
            Temporizador = 0; 
            ControlPanel(false);
            ControlVision(false); 
        }
        else if (estado == Estado.inactivo && Temporizador >=DuracionDeDesactivacion)
        {
            estado =Estado.activo;
            Temporizador = 0;
            ControlPanel(true);
            ControlVision(true); 
        }
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

}

 // class Armario 
// namespace

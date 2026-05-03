//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Teleport : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [SerializeField]
    Vector3 TargetDestination;
    [SerializeField]
    Player_Controller player;
    [SerializeField]
    Camera Camera;
    // En caso de que  queramos en algún momento teletrasportar automáticamente al jugador
    [SerializeField]
    bool AutomaticTelepor;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    #endregion
    private InputAction Tele;
    private bool InteractPuss;
    private AudioSource _clip;
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>

    private void Start()
    {
        if (player == null)
        { Debug.Log("NO HAY JUGADOR CONFIGURADO EN ESTE TP"); }
        if (Camera == null)
        { Debug.Log("NO HAY CÁMARA CONFIGURADA EN ESTE TP"); }
        Tele = InputSystem.actions.FindAction("Interact");
        if (Tele == null)
        {
             Debug.Log("No encontrada acción interact"); 
        }
        if (this.GetComponent<AudioSource>() != null)
        {
            _clip = this.GetComponent<AudioSource>();
        }
        else
        {
            Debug.Log("y el audio carnal?");
        }
    }
    //private void Update()
    //{
    //    if (!AutomaticTelepor)
    //    {
    //        if (Tele.WasPressedThisFrame())
    //        {
    //            Debug.Log("pulsar ha sido pulsado");
    //            InteractPuss = true;
    //        }
    //    }
       
    //}

    #endregion
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    Debug.Log("entran");
    //    if (!collision.GetComponent<Player_Controller>())
    //    {
    //        return; 
    //    }
    //    else if (InteractPuss && !AutomaticTelepor)
    //    {
    //        Debug.Log("pulsar ha sido pulsado");
    //        Tp();
    //        InteractPuss = false;
    //    }
    //    else if (AutomaticTelepor)
    //    {
    //        Tp();
    //    }
    //}
   

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public void Tp()
    {
        player.transform.position = TargetDestination;
        Vector3 camAux = Camera.transform.position;
        camAux.x = TargetDestination.x;
        camAux.y = TargetDestination.y+2;
        Camera.transform.position = camAux;
        _clip.Play();
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class Teleport 
// namespace

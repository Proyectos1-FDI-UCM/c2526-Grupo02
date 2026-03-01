//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
// Añadir aquí el resto de directivas using



public class Interactuable : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    //Componente que se usa para comprobar si la camara esta mirando arriba
    [SerializeField]
    private LookUp LookUpComponent;

    //Evento que se llama cuando interactuas
    [SerializeField]
    private UnityEvent OnInteract;

    //Booleano usado para controlar si la interaccion la lleva a cabo la camara o el jugador
    [SerializeField]
    [Tooltip("Bool que controla si la interaccion la hace la camara o el jugador")]
    private bool cameraInteracts = true;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    //Variable que guarda la accion de interact
    private InputAction _interact;

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour


    void Start()
    {
        _interact = InputSystem.actions.FindAction("Interact"); //asignamos la accion
        if (_interact == null)
        {
            Debug.Log("No se ha encontrado la acción Interact");
            return;
        }
        if(LookUpComponent == null)
        {
            Debug.Log("Falta asignar el lookUp de la camara");
        }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (((!cameraInteracts && !LookUpComponent.GetAlturaAlta() && other.GetComponent<Player_Controller>()) //Interaccion del jugador, la camara no esta mirando arriba y el jugador esta en rango
            || (cameraInteracts && LookUpComponent.GetAlturaAlta() && other.GetComponentInParent<Camera>())) //Interaccion de la camara, la camara esta mirando arriba y esta en rango
            && _interact.WasPressedThisFrame()) //Si el jugador esta pulsando el boton de interaccion
        {
            OnInteract.Invoke(); //llamamos a la funcion asignada en el inspector
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

} // class Interactuable 
// namespace

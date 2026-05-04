//---------------------------------------------------------
// Gestiona la interacción entre el jugador y objetos del juego y permite ejecutar un evento configurable
// Alejandra 
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
// Añadir aquí el resto de directivas using


/// <summary>
/// El bool cameraInteracts sive para que si esta en true, solo se pueda interactuar con el objeto cuando se está mirando hacia arriba. Sin embargo, si
/// este está desactivado permite interactuar con el objecto, en este caso al interactuar, permite llamar a una función cualquiera para que se ejecute.
/// </summary>
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
    private InputAction _Interact;
   
    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// <summary>
    void Start()
    {
        //Programación defensiva para evitar pequeños despistes
        //Si no está ninguna de las acciones se avisa mediante consola
        _Interact = InputSystem.actions.FindAction("Interact"); //asignamos la accion
        if (_Interact == null)
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
        
        if(!Pausa_controller.IsGamePaused)
        {
                if (((!cameraInteracts && !LookUpComponent.GetAlturaAlta() && other.tag == "Player") //Interaccion del jugador, la camara no esta mirando arriba y el jugador esta en rango
                    || (cameraInteracts && LookUpComponent.GetAlturaAlta() && other.tag == "Player")) //Interaccion de la camara, la camara esta mirando arriba y esta en rango
                    && _Interact.WasPressedThisFrame()) //Si el jugador esta pulsando el boton de interaccion
                {
                OnInteract.Invoke(); //llamamos a la funcion asignada en el inspector
                }
            
        }
    }
    #endregion
} // class Interactuable 
// namespace

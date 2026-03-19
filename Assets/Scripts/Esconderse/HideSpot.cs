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

//Representa un escondite interactuable en el juego.
//Permite al jugador activar el estado de escondido cuando está
//dentro del rango y realiza la acción de interactuar.

public class HideSpot : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    /// <summary>
    /// Documentar cada atributo que aparece aquí.
    /// </summary>
    /// El convenio de nombres de Unity recomienda que los atributos
    /// públicos y de inspector se nombren en formato PascalCase
    /// (palabras con primera letra mayúscula, incluida la primera letra)
    /// Ejemplo: MaxHealthPoints
    
    //posición para esconderse
    // Transform opcional que indica la posición exacta
    // donde el jugador debe colocarse al esconderse.
    //Si no se asigna, se usará la posición del propio objeto.
    [SerializeField] private Transform hidePoint;
    #endregion

    // Referencia al jugador que se encuentra cer del escondite.
    // Se asigna al entrar en el trigger y se limpia al salir.
    private GameObject playerInRange;

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// Por defecto están los típicos (Update y Start) pero:
    /// - Hay que añadir todos los que sean necesarios
    /// - Hay que borrar los que no se usen 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    ///
    
    //Se llama uando un collider entra en el trigger 2D
    // del escondite. Se comprueba si el objeto que entra es un jugador 
    // que tiene HideSystem y se guarda la referencia.
    private void OnTriggerEnter2D(Collider2D other)
    {
        HideSystem hide = other.GetComponent<HideSystem>();
        if (hide != null)
        {
            playerInRange = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == playerInRange)
        {
            // Guardamos el jugador en rango para usarlo al interactuar
            playerInRange = null;
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// Documentar cada método que aparece aquí con ///<summary>
    /// El convenio de nombres de Unity recomienda que estos métodos
    /// se nombren en formato PascalCase (palabras con primera letra
    /// mayúscula, incluida la primera letra)
    /// Ejemplo: /// Método que se llama al interactuar con el escondite.
    
    // Alterna el estado de escondido del jugador si este se encuentra
    // dentro del rango. Se conecta con el HideSystem del jugador.

    public void Interact()
    {
        // No hacer nada si no hay jugador en rango
        if (playerInRange == null) return;
        
        // Obtener el HideSystem del jugador
        HideSystem hideSystem = playerInRange.GetComponent<HideSystem>();
        if (hideSystem == null) return;
        
        // Determinar el punto donde el jugador se debe esconder
        Transform point = hidePoint != null ? hidePoint : transform;

        // Llamar al método que alterna el estado de escondido
        hideSystem.ToggleHide(point);
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class NewMonoBehaviourScript 
// namespace

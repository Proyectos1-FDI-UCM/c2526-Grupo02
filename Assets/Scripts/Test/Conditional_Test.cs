//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Conditional_Test : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
  
    [SerializeField]
    private UnityEvent code;
    [SerializeField]
    private UnityEvent negativeCode;
    [SerializeField]
    private Object.ItemType NeededType;
    [SerializeField]
    private GameObject mano;


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
        if (mano == null)
        {
            Debug.Log("No hay configurada Mano");
        }
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    
    public void Check()
    {
        //Ponemos un tipo que nunca va a poder ser
        Object.ItemType type = Object.ItemType.numItemTypes;

        //Revisamos en el componente Object use de mano el objeto que es (sprite + tipo), y de ahí sacamos el tipo de item.
        //IMPORTANTE (si usabamos directamente el item type nos daba un no instance of an object, ya que los enum no pueden ser nulos)
        //Resumen, si hay objeto, sacamos el tipo de objeto
        if (mano.GetComponent<Object_use>().GetHandItem() != null)
        {
         type = mano.GetComponent<Object_use>().GetHandItem().GetItem();
        }

        if (type == NeededType)
        {
            code.Invoke();
        }
        else
        {
            negativeCode.Invoke();
            Debug.Log("MAAAAAAAL");
        }
    }
#endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class Conditional_Test 
// namespace

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
    [Header("Configuramos el tipo de objeto y la flag que queremos, " +
        "en INTERACTUABLE llamaremos a alguna de las tres " +
        "funciones que nos permtie ver si tenemos el objeto" +
        ", si se cumple la condición o ambas")]
    [SerializeField]
    private UnityEvent code;
    [SerializeField]
    private UnityEvent negativeCode;
    [SerializeField]
    private Object.ItemType NeededType;
    [SerializeField]
    private GameObject mano;
    [SerializeField]
    private Flags.Conditions condition;


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
        if(GameManager.Instance == null)
        {
            Debug.Log("No hay gameManager");
        }
        
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
            Debug.Log("Bien");
        }
        else
        {
            Debug.Log("Mal");
            negativeCode.Invoke();
           
        }
    }
    //Se llama a la Flag correspondiente y se ve si es verdadera y falsa, entonces se ejecuta un código u otro
    public void Check_Condition()
    {
        Flags flag = GameManager.Instance.GetFlags();
        if (flag.GetPos(condition))
        {
            code.Invoke();
        }
        else
        {
            negativeCode.Invoke();

        }
    }

    public void Check_Both()
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
        Flags flag = GameManager.Instance.GetFlags();
        if (type == NeededType && flag.GetPos(condition))
        {
            code.Invoke();
        }
        else
        {
            negativeCode.Invoke();

        }
    }
#endregion
    

} // class Conditional_Test 
// namespace

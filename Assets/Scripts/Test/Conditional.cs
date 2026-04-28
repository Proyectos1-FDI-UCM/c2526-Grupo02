//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Alejandro Jiménez Rojo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
// Añadir aquí el resto de directivas using



public class Conditional : MonoBehaviour
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
    private UnityEvent code; //Métodos a los que llamar cuando se cumple la condición
    [SerializeField]
    private UnityEvent negativeCode;//Métodos a los que llamar cuando no se cumple la condición
    [SerializeField]
    private GameObject Mano; //Mano de la que revisar que objeto tenemos equipado
    [SerializeField]
    private Object.ItemType NeededType; //Objeto necesitado
    //Usa condiciones y que condición usa
    [SerializeField]
    private Flags.Conditions condition; //Condición que se debe cumplir
    [SerializeField]
    private bool Repeateable; //Atributo que nos dice si se puede repetir (si se cumple una vez, puedes volver a necesitar cumplirla)
    #endregion
    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private bool _used = false; //variable que nos dice si ha sido usado una vez
    #endregion
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Start()
    {
        //Programación defensiva
        if (Mano == null)
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
    //Método para revisar si tenemos un objeto concreto seleccionado en la mano
    public void CheckObjectInHand()
    {
        //Ponemos un tipo que nunca va a poder ser
        Object.ItemType type = Object.ItemType.numItemTypes;

        //Revisamos en el componente Object use de mano el objeto que es (sprite + tipo), y de ahí sacamos el tipo de item.
        //IMPORTANTE (si usabamos directamente el item type nos daba un no instance of an object, ya que los enum no pueden ser nulos)
        //Resumen, si hay objeto, sacamos el tipo de objeto
        if (Mano.GetComponent<Object_use>().GetHandItem() != null)
        {
         type = Mano.GetComponent<Object_use>().GetHandItem().GetItem();
        }

        if (type == NeededType || (!Repeateable && _used))
        {
            code.Invoke();
            Debug.Log("Bien");
            _used = true;
        }
        else
        {
            Debug.Log("Mal");
            negativeCode.Invoke();
           
        }
    }
    //Se revisa si la flag correspondiente es válida
    public void CheckCondition()
    {
        Flags flag = GameManager.Instance.GetFlags();
        if (flag.GetPos(condition) || (!Repeateable && _used))
        {
            code.Invoke();
            _used = true;
        }
        else
        {
            negativeCode.Invoke();

        }
    }

    //Método que revisa si tienes un objeto en el inventario
    public void CheckInventory()
    {

    }
#endregion
    

} // class Conditional_Test 
// namespace

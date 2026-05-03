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
    private Object_use Mano; //Mano de la que revisar que objeto tenemos equipado, es del tipo Object_use ya que necesitamos el componente bject_use y asi no tenemos que hacer GetComponent<>
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

    private bool _used = false; //atributo que nos dice si ha sido usado una vez
    private Flags _flags; //atributo que nos da el compoennte _flags del level manager (cachear)
    private Object.ItemType _type; //atributo que nos define un objeto que vamos a usar para revisar que objeto tenemos en la mano (para no crearla cada vez que llamemos a la función)
    private Inventory_Manager _inv; //atributo en el que almacenaremos el inventario, para no llamarlo cada vez que llamemos al método que lo necesita.
    #endregion
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Start()
    {
        //Programación defensiva
        if (Mano == null)
        {
            Debug.Log("No hay configurada Mano");
            return;
        }
        if(GameManager.Instance == null)
        {
            Debug.Log("No hay gameManager");
            return;
        }
        //Cogemos el inventario del level manager y si no hay avisamos
        //_inv = GameManager.Instance.GetInv().GetComponent<Inventory_Manager>();
        if (_inv == null)
        {
            Debug.Log("No hay inventory manager en el level manager");
        }
        //Cogemos el componente _flags del level manager y si no hay avisamos
        //_flags = GameManager.Instance.GetFlags();
        if (_flags == null)
        {
            Debug.Log("No hay flags configuradas, revisar LevelManager");
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    //Método para revisar si tenemos un objeto concreto seleccionado en la mano
    public void CheckObjectInHand()
    {
        //Revisamos en el componente Object use de mano el objeto que es (sprite + tipo), y de ahí sacamos el tipo de item.
        //IMPORTANTE (si usabamos directamente el item type nos daba un no instance of an object, ya que los enum no pueden ser nulos)
        //Resumen, si hay objeto, sacamos el tipo de objeto

        //Si no hay un objeto en la mano no lo asigna
        if (Mano.GetHandItem() != null)
        {
            _type = Mano.GetHandItem().GetItem();
        }
        else
        {
            //asignamos un tipo de item que nunca será
            _type = Object.ItemType.numItemTypes;
        }
            




        if (_type == NeededType || (!Repeateable && _used))
        {
            code.Invoke();
            _used = true;
        }
        else
        {
            negativeCode.Invoke();
        }
    }
    //Se revisa si la flag correspondiente es válida
    public void CheckCondition()
    {
        if ((!Repeateable && _used) || _flags.GetPos(condition))
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
       
        if ((!Repeateable && _used) || _inv.CheckObject(NeededType))
        {
            code.Invoke();
            _used = true;
        }
        else
        {
            negativeCode.Invoke();
        }
    }
#endregion
    

} // class Conditional_Test 
// namespace

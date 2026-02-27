//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Inventory_Manager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    private GameObject InvHud;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private int _invLenght = 5;
    private int _nObj = 0;
    [SerializeField]
    private Object[] inv;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    void Start()
    {
        //Programación defensiva para  ver que la hud del inventario este configurada
        if (InvHud != null)
        {
            Debug.Log("No hay ningún hud configurado para el inventario");
        }
        //Creamos el inventario (array de Object)
        inv = new Object[_invLenght];

    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    //Método que nos permite crear un objeto dotándole el tipo de objeto que es y la descripción.
    public Object CreateObj(objType obj, string desc)
    {
        Object o = new Object();
        o.SetDesc(desc);
        //Hay que plantearnos quitar el index
        o.SetIndex(_nObj);
        o.SetObj(obj);
        return o;
    }
    //Método que busca un espacio libre en el inventario (Dado por el último nObj) y añade el objeto (que se habrá creado con CreateObj)
    public void AddObj(Object Object)
    {
        //Va la posición _objeto y añade ese objeto en el inventario, a no ser que este lleno (_nObj no es menor a la longitud del inventario)
        if (_nObj < _invLenght)
        {
            inv[_nObj] = Object;
            //Si el objeto no es del tipo vacio añade uno a _nObj,
            if (Object.Getobj() != objType.vacio)
            {
                _nObj++;
            }

        }
        else { Debug.Log("INVENTARIO LLENO"); }
    }
    //Método que busca un tipo de objeto en el inventario y si lo encuentra lo borra y desplaza todos los posteriores al final
    public void RemoveObj(objType obj)
    {
        //No se si hay otra manera que no sea una búsqueda, la idea de los indices no se como funcionaría ya que no se como acceder a un objeto según su indice
        int i = 0;
        bool encontrado = false;
        bool acabado = false;
        //Si el objeto es igual al que queremos salimos del bucle
        while (i < _invLenght && !encontrado)
        {
            encontrado = (inv[i].Getobj() == obj);
            i++;
        }
        //Si el objeto es encontrado lo quitamos y desplazamos a la izquierda todo lo siguiente
        if (encontrado)
        {
            while (i < _invLenght && !acabado)
            {
                inv[i - 1] = inv[i];
                //acaba cuando el siguiente del inventario es vacio
                acabado = inv[i].Getobj() == objType.vacio;
                i++;
            }
            if (!acabado)
            {
                inv[i - 1] = CreateObj(objType.vacio, "");
            }

        }
        else
        {
            Debug.Log("No se encontró objeto");
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

} // class Inventory_Manager 
// namespace

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

    //longitud del inventario
    private int _invLenght = 5;
    //indice del ultimo hueco vacio
    private int _nObj = 0;
    //array donde se guardan los objetos
    //[SerializeField] //por si se quiere ver que objetos hay en el inventario
    private Object[] _inv;
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
        _inv = new Object[_invLenght];

    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    //Método que busca un espacio libre en el inventario (Dado por el último nObj) y añade el objeto que se le pasa
    public void AddObj(Object Object)
    {
        if (_nObj < _invLenght) //para no coger objetos con el inventario lleno, despues lo añadimos y quitamos del mundo
        {
            _inv[_nObj] = Object;
            _nObj++;
            Object.RemoveFromWorld(); //lo quitamos del mundo
        }
    }

    //Al usar UnityEvents serializados, no se pueden usar funciones que tengan como parametro un enum,
    //por lo que pasamos un int y casteamos al enum
    public void RemoveFromInv(int itemType)
    {
        if (itemType < (int)Object.ItemType.numItemTypes) 
        {
            RemoveObj((Object.ItemType)itemType);
        }
        else
        {
            Debug.Log("Tipo de item no válido");
        }
    }


    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    //Método que busca un tipo de objeto en el inventario y si lo encuentra lo borra y desplaza todos los posteriores hacia delante
    private void RemoveObj(Object.ItemType obj)
    {
        bool encontrao = false;
        int i = 0;
        while (!encontrao && i < _nObj) //buscamos el objeto
        {
            if (_inv[i].GetItem() == obj)
            {
                encontrao = true;
                //TODO: añadir el objeto a la mano
            }
            else
            {
                i++;
            }
        }

        for (int j = i; j < _nObj; j++) //desplazamos todos los objetos para rellenar el hueco del objeto borrado
        {
            _inv[j] = _inv[j + 1];
        }
        _nObj--;
    }
    #endregion

} // class Inventory_Manager 
// namespace

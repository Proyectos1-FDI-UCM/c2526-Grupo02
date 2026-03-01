//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
// Añadir aquí el resto de directivas using



public class Inventory_Manager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    //los huecos donde van los objetos en el hud del inventario 
    [SerializeField]
    private GameObject[] _invHudSpaces;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)


    //longitud del inventario
    private int _invLenght = 5;

    //indice del ultimo hueco vacio
    private int _nObj = 0;

    //array donde se guardan los objetos
    [SerializeField] //por si se quiere ver que objetos hay en el inventario
    private Object[] _inv;

    //input de abrir/cerrar el inventario
    private InputAction _openInvAction;

    //booleano que indica si el inventario esta abierto
    private bool _inventoryIsOpen = false;

    //objet de la ui que tiene todo el inventario abierto (mas facil de ocultar/mostrar asi)
    private GameObject _inventoryHud;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour


    void Start()
    {
        //Programación defensiva para  ver que la hud del inventario este configurada
        if (_invHudSpaces.Length != _invLenght)
        {
            Debug.Log("Hud no configurado para el inventario");
        }
        //mu feo pero asi te quitas asignarlo en el inspector
        _inventoryHud = _invHudSpaces[0].transform.parent.gameObject.transform.parent.gameObject;


        _openInvAction = InputSystem.actions.FindAction("Inventory"); //asignamos la accion
        if (_openInvAction == null)
        {
            Debug.Log("No se ha encontrado la acción Inventario");
            return;
        }

        //Creamos el inventario (array de Object)
        _inv = new Object[_invLenght];
    }

    private void Update()
    {
        if (_openInvAction.WasPressedThisFrame()) //si pulsamos el boton de inventario lo mostramos/ocultamos
        {
            _inventoryIsOpen = !_inventoryIsOpen;
            _inventoryHud.SetActive(_inventoryIsOpen);
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    //Método que busca un espacio libre en el inventario (Dado por el último nObj) y añade el objeto que se le pasa
    public void AddObj(Object Object)
    {
        if (_nObj < _invLenght) //para no coger objetos con el inventario lleno, despues lo añadimos y quitamos del mundo
        {
            _inv[_nObj] = Object;
            _invHudSpaces[_nObj].GetComponent<UnityEngine.UI.Image>().sprite = Object.GetInventorySprite();
            _invHudSpaces[_nObj].SetActive(true);
            _nObj++;
            Object.RemoveFromWorld(); //lo quitamos del mundo
        }
    }

    //Al usar UnityEvents serializados, no se pueden usar funciones que tengan como parametro un enum,
    //por lo que pasamos un int y casteamos al enum
    public void RemoveFromInv(int itemType)
    {
        if (itemType < (int)Object.ItemType.numItemTypes) //comprobamos que el indice del enum ea valido
        {
            //Debug.Log((Object.ItemType)itemType);
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

    //Método que busca un tipo de objeto en el inventario y si lo encuentra lo borra y desplaza todos los posteriores hacia delante
    private void RemoveObj(Object.ItemType obj)
    {
        bool encontrao = false;
        int i = 0;
        while (!encontrao && i < _nObj) //buscamos el objeto
        {
            if (_inv[i].GetItem() == obj)
            {
                Debug.Log("Soy: " + _inv[i].GetItem());
                encontrao = true;
                //TODO: añadir el objeto a la mano
            }
            else
            {
                i++;
            }
        }

        if (encontrao) //si no encontramos el objeto no lo podemos borrar
        {
            //desplazamos los objetos hacia la izquierda y cuando llegamos al ultimo objeto que haya en el inventario paramos
            //(porque no podemos copiar a ese algo vacio/que se sale del array)
            for (int j = i; j < _nObj - 1; j++) 
            {
                _inv[j] = _inv[j + 1];
                _invHudSpaces[j].GetComponent<UnityEngine.UI.Image>().sprite = _inv[j].GetInventorySprite();
            }

            //actualizamos el numero de objetos en el inventario y liberamos el ultimo objeto que ahora es un hueco
            //(porque lo desplazamos a la izquierda antes)
            _nObj--;
            _inv[_nObj] = null;
            _invHudSpaces[_nObj].GetComponent<UnityEngine.UI.Image>().sprite = null;
            _invHudSpaces[_nObj].SetActive(false);
        }
    }
    #endregion

} // class Inventory_Manager 
// namespace

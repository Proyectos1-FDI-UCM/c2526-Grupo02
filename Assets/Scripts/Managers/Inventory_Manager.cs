//---------------------------------------------------------
// Breve descripción del contenido del archivo - Alejandra
// Responsable de la creación de este archivo - Maneja el inventario
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

    //objet de la ui que tiene todo el inventario abierto (mas facil de ocultar/mostrar asi)
    [SerializeField]
    private GameObject _inventoryHud;

    //los huecos donde van los objetos en el hud del inventario 
    [SerializeField]
    private UnityEngine.UI.Image[] _invHudSpaces;

    //índice :D
    [SerializeField]
    private int _currentItemIndex = 0;
    [SerializeField]
    private Object _sujetado;

    [SerializeField]
    private float _delayTime = 0.5f;

    [SerializeField]
    private RectTransform _selection;
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

    private InputAction _move;
    private InputAction _Interact;

    //booleano que indica si el inventario esta abierto
    private bool _inventoryIsOpen = false;

    //ref al jugador
    private GameObject _player;

    private float _timer = 0.0f;
    #endregion


    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour


    void Start()
    {
        //Programación defensiva para ver que la hud del inventario esté configurada
        if (_invHudSpaces.Length != _invLenght)
        {
            Debug.Log("Hud no configurado para el inventario");
        }

        _openInvAction = InputSystem.actions.FindAction("Inventory"); //asignamos la accion
        if (_openInvAction == null)
        {
            Debug.Log("No se ha encontrado la acción Inventario");
            return;
        }
        _move = InputSystem.actions.FindAction("Move");
        if (_move == null)
        {
            Debug.Log("No se ha encontrado la acción move");
            return;
        }

        _Interact = InputSystem.actions.FindAction("Interact"); //asignamos la accion
        if (_Interact == null)
        {
            Debug.Log("No se ha encontrado la acción Interact");
            return;
        }

        //Creamos el inventario (array de Object)
        _inv = new Object[_invLenght];
        _player = GameManager.Instance.GetPlayer();
    }

    private void Update()
    {
        if (_openInvAction.WasPressedThisFrame()) //si pulsamos el boton de inventario lo mostramos/ocultamos
        {
            _inventoryIsOpen = !_inventoryIsOpen;
            _inventoryHud.SetActive(_inventoryIsOpen);
            if (_inventoryIsOpen)
            {
                _currentItemIndex = 0;
                _timer = _delayTime;
                _player.GetComponent<Player_Controller>().Stop();
            }
            else
            {
                _player.GetComponent<Player_Controller>().Resume();
            }

        }

        if (_inventoryIsOpen)
        {
            //logica inputs inventario aqui
            Vector2 dir = _move.ReadValue<Vector2>();
            float HorizontalDir = Mathf.Round(dir.x);

            if(dir.x != 0)
            {
                if (_timer >= _delayTime)
                {
                    if (_currentItemIndex > 0 && HorizontalDir == -1)
                    {
                        _currentItemIndex--;
                    }
                    else if (_currentItemIndex < _invLenght - 1 && HorizontalDir == 1)
                    {
                        _currentItemIndex++;
                    }
                    _selection.position = _invHudSpaces[_currentItemIndex].GetComponent<RectTransform>().position;

                    _timer = 0;
                }
                else
                {
                    _timer += Time.deltaTime;
                }
            }

            //logica interfaz mover al seleccionado

            if (_Interact.WasPressedThisFrame() && _currentItemIndex < _nObj)
            {

                _player.GetComponent<Object_use>().ObjetoRecogido(_inv[_currentItemIndex]);
                _sujetado = _inv[_currentItemIndex];
            }
        }
    }

    #endregion


    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    /// <Summary>
    /// Método que busca un espacio libre en el inventario 
    /// (Dado por el último nObj) 
    /// y añade el objeto que se le pasa
    /// <Summary>
    public void AddObj(Object Object)
    {
        if (_nObj < _invLenght) //para no coger objetos con el inventario lleno, despues lo añadimos y quitamos del mundo
        {
            _inv[_nObj] = Object;
            _invHudSpaces[_nObj].sprite = Object.GetInventorySprite();
            _invHudSpaces[_nObj].gameObject.SetActive(true);
            _nObj++;
            Object.RemoveFromWorld(); //lo quitamos del mundo
        }
    }


    /// <Summary>
    ///Al usar UnityEvents serializados, no se pueden usar funciones que tengan como parametro un enum,
    ///por lo que pasamos un int y casteamos al enum
    /// <Summary>
    public void RemoveFromInv(int itemType)
    {
        if (itemType < (int)Object.ItemType.numItemTypes) //comprobamos que el indice del enum ea valido
        {
            //Debug.Log((Object.ItemType)itemType);
            RemoveObj((Object.ItemType)itemType);
            _player.GetComponent<Object_use>().RemoveFromHand();
        }
        else
        {
            Debug.Log("Tipo de item no válido");
        }
    }
    public int IniState ()
    {
        return _invLenght;
    }
    public Object RetState(int n) // regresa el estado del inventario
    {
        return _inv[n];
    }
    public void LoadState(Object[] old)
    {
        _nObj = 0;
        for (int i = 0; i < _invLenght; i++)
        {
            _inv[i] = old[i];
            _invHudSpaces[i].gameObject.SetActive(true);
            _invHudSpaces[i].sprite = null;
            if (old[i] != null)
            {  
                _invHudSpaces[i].sprite = old[i].GetInventorySprite();
                _nObj++;
            }
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
                _invHudSpaces[j].sprite = _inv[j].GetInventorySprite();
            }

            //actualizamos el numero de objetos en el inventario y liberamos el ultimo objeto que ahora es un hueco
            //(porque lo desplazamos a la izquierda antes)
            _nObj--;
            _inv[_nObj] = null;
            _invHudSpaces[_nObj].sprite = null;
            _invHudSpaces[_nObj].gameObject.SetActive(false);
        }
    }
    #endregion

} // class Inventory_Manager 
// namespace

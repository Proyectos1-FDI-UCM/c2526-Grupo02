//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo -Hermes -Alejandro
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Lee un archivo elegido desde el editor y lo escribe línea por línea en un canvas.
/// </summary>
public class Dialogo : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    
    [SerializeField]
    private Canvas Canvas; //caja del texto
    [SerializeField]
    private Text Dialogue; //el texto
    [SerializeField]
    private float Speed = 0.03f;//velocidad del texto
    //Atributos para los distintos audios
    [SerializeField]
    private AudioClip High;
    [SerializeField]
    private AudioClip Middle;
    [SerializeField]
    private AudioClip Low;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private string[] _script; //las lineas de dialogo
    //private string[] _scriptCon; //lineas del dialogo alternativo
    private string _currentLine;
    private int _line = 0; //el indice para las lineas de dialogo
    //private bool _puzzle; //activa el dialogo fuera del input
    private bool _type = false; //está escribiendo la linea
    private bool _typeAll = false; //escirbe la linea completa
    private float _time;    //contador
    private InputAction _talk;  //input de interactuar
    private bool _talking;  //permite interactuar cuando el jufgador se encuentra en el area indicada
    private int _index = 0; // puntero para recorrer cada línea
    private bool _pressed;  
    private bool _first;
    private string _name = string.Empty;
    private Player_Controller _playerController;
    private AudioSource _clip;
    private string _archive;
    private string _ruta;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// sistema de dialogo, usa un archivo de texto y lee el contenido y lo muestra linea por linea
    void Start()
    {
        _playerController = GameManager.Instance.GetPlayer().GetComponent<Player_Controller>();
        Canvas.enabled = false;

        //Con esto llamamos al componente que hará el ruido cuando se hable (como siempre que hablemos con alguien estaremos cerca, ponemos el audio source al propio del jugador)
        if (_playerController.GetComponent<AudioSource>() != null)
        {
            _clip = _playerController.GetComponent<AudioSource>();
        }
        else
        {
            Debug.Log("y el audio carnal?");
        }
        _talk = InputSystem.actions.FindAction("Interact"); //asignamos la accion
    }
    void Update()
    {
            if (_talking)
            {
                _playerController.Stop();
               
                if (_line == _script.GetLength(0) + 1 )
                {
                    Canvas.enabled = false;
                    _line = 0;
                    _talking = false;
                    _playerController.Resume();
                }//si avanza dialogo en la ultima linea regresa al estado de entrada
                _time += Time.deltaTime;
                //Código para que la primera línea se ejecute sin tener que "interactuar"
                if (_first)
                {
                    Debug.Log("PRIMERA");
                    _pressed = true;
                    _first = false;
                }
                else
                {
                    _pressed = _talk.ReadValue<float>() > 0f && _talk.WasPressedThisFrame();
                }

                if (_pressed)
                {
                    Canvas.enabled = true;
                    if (_type) _typeAll = true;
                    else
                    {
                        if (_line < _script.GetLength(0))
                        {
                            WriteLine(_script[_line]);
                        }
                        _line++;
                    }
                }
            }
            else
            {
                _line = 0;
                
            }
            if (_type)
            {
                if (_typeAll)
                {
                    Dialogue.text = $"{_name} {_currentLine}";
                    _type = false;
                    _typeAll = false;
                    return;
                }
                else
                {
                    _time += Time.deltaTime;

                    if (_time >= Speed && _index < _currentLine.Length)
                    {
                        Dialogue.text += _currentLine[_index];
                        if (_index % 2 == 0) _clip.PlayOneShot(High);
                        else if (_index % 3 == 0) _clip.PlayOneShot(Middle);
                        else _clip.PlayOneShot(Low);
                        
                        
                        _index++;
                        _time = 0;
                    }

                    if (_index >= _currentLine.Length)
                    {
                        _type = false;
                    }
                }
            }
        
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public bool IsTalking() // avisa a otros componentes si el jugador está hablando
    {
        return _talking;
    }

    //Necesitamso dividirlo en dos scripts porque los unity events no permiten más de un parámetro
    //MÉTODOS NECESARIOS PARA EL DIALOG MANAGER

    //Método con el que ponemos el nombre del personaje que habla
    public void SetName(string inputName)
    {
        _name = inputName;
    }
    //Método con el que ponemos la velocidad a la que habla
    public void SetSpeed(float spd)
    {
        Speed = spd;
    }
    //Método al que le pasamos el nombre del archivo y hace que hable
    public void Talk(string fileName) // inicializa las variables necesarias para empezar el dialogo
    {
        if (!_talking)
        {
            _talking = true;
            _first = true;
            _currentLine = "";
             _ruta = Path.Combine(Application.streamingAssetsPath, fileName);
            if (File.Exists(_ruta))
            {
                string texto = File.ReadAllText(_ruta); // lee el archivo
                _script = texto.Split('\n'); //lo separa por lineas
                Debug.Log(texto.Length);
            }
            else
            {
                Debug.LogError("No se encontró el archivo en: " + _ruta);
                Debug.Log(this.gameObject.name);
            }
           

        }
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<Player_Controller>() != null) { _talking = true; }
    //}
    private void OnTriggerExit2D(Collider2D collision) // resetea todo  al alejarse del npc
    {
        if (collision.GetComponent<Test_detect_correction>() != null) 
        {
            _talking = false; 
            Canvas.enabled = false; 
            _currentLine = ""; 
        }
    }
    private void WriteLine(string linea) //animacion de escribir por letra, el nombre del personaje se queda afuera del bucle para que no cambie
    {
        _index = 0;
        _time = 0;
        _currentLine = linea;
        Dialogue.text = $"{_name}: ";
        _type = true;
    }
}

#endregion

// class Dialogo 
// namespace
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
    [SerializeField]
    private string Name; //nombre del npc
    [SerializeField]
    private string Archive;//Nombre del archivo + .txt
    [SerializeField]
    private string ArchiveCon; // Dialogo secundario
    [SerializeField]
    private bool Flee; //si el npc desaparece después de terminar su dialogo
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private string[] _script; //las lineas de dialogo
    private string[] _scriptCon; //lineas del dialogo alternativo
    private string _currentLine;
    private int _line = 0; //el indice para las lineas de dialogo
    private bool _puzzle; //activa el dialogo fuera del input
    private bool _type = false; //está escribiendo la linea
    private bool _typeAll = false; //escirbe la linea completa
    private float _time;    //contador
    private InputAction _talk;  //input de interactuar
    private bool _talking;  //permite interactuar cuando el jufgador se encuentra en el area indicada
    private int _index = 0; // puntero para recorrer cada línea
    private bool _pressed;  
    private bool _first;
    private bool _fleed = false;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// sistema de dialogo, usa un archivo de texto y lee el contenido y lo muestra linea por linea
    void Start()
    {
        Canvas.enabled = false;

        string ruta = Path.Combine(Application.streamingAssetsPath, Archive);
        string rutacon = Path.Combine(Application.streamingAssetsPath, ArchiveCon);

        if (File.Exists(ruta))
        {
            string texto = File.ReadAllText(ruta); // lee el archivo
            _script = texto.Split('\n'); //lo separa por lineas
            Debug.Log(texto.Length);
        }
        else
        {
            Debug.LogError("No se encontró el archivo en: " + ruta);
            Debug.Log(this.gameObject.name);
        }
        if (File.Exists(rutacon))
        {
            string textocon = File.ReadAllText(rutacon); // lee el archivo
            _scriptCon = textocon.Split('\n'); //lo separa por lineas
            Debug.Log(textocon.Length);
        }
        else
        {
            Debug.LogError("No se encontró el archivo en: " + rutacon);
        }
        _talk = InputSystem.actions.FindAction("Interact"); //asignamos la accion
        _puzzle = false;
    }
    void Update()
    {
        if (!_fleed)
        {
            if (_talking)
            {
                if (_line == _script.GetLength(0) + 1 || _line == _scriptCon.GetLength(0) + 1)
                {
                    Canvas.enabled = false;
                    _line = 0;
                    _talking = false;
                    _fleed = _puzzle && Flee;

                }//si avanza dialogo en la ultima linea regresa al estado de entrar
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
                        if (!_puzzle && _line < _script.GetLength(0))
                        {
                            WriteLine(_script[_line]);
                        }
                        else if (_puzzle && _line < _scriptCon.GetLength(0))
                        {
                            WriteLine(_scriptCon[_line]); 
                        }
                        _line++;
                    }// comprueba cual archivo debe mostrar y pasa la linea a un método privado, después suma uno al puntero
                }
            }
            else
            {
                //  Canvas.enabled = false;
                _line = 0;
                
            }
            if (_type)
            {
                if (_typeAll)
                {
                    Dialogue.text = $"{Name}: {_currentLine}";
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
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    public void PuzzleSwitch(bool sel) // altera el valor de _puzzle para cambiar de archivo desde otros componentes 
    {
        _puzzle = sel;
    }

    public bool IsTalking() // avisa a otros componentes si el jugador está hablando
    {
        return _talking;
    }

    public void Talk() // inicializa las variables necesarias para empezar el dialogo
    {
        if (!_talking)
        {
            _talking = true;
            _first = true;
            _currentLine = "";
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
        Dialogue.text = $"{Name}: ";
        _type = true;
    }
}

#endregion

// class Dialogo 
// namespace
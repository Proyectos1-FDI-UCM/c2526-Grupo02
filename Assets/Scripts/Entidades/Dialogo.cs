//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
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
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Dialogo : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints
    [SerializeField]
    private Canvas Canvas; //caja del texto
    [SerializeField]
    private Text dialogo; //el texto
    [SerializeField]
    private float Velocidad = 0.03f;//velocidad del texto
    [SerializeField]
    private string Nombre; //nombre del npc
    [SerializeField]
    private string Archivo;//Nombre del archivo + .txt
    [SerializeField]
    private string ArchivoCon; // Dialogo secundario
    [SerializeField]
    private bool Huye;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private string[] _script; //las lineas de dialogo
    private string[] _scriptCon; //lineas del dialogo alternativo
    private string _currentScript;
    private int _line = 0; //el indice para las lineas de dialogo
    private bool _puzzle; //activa el dialogo fuera del input
    private bool _type = false; //está escribiendo la linea
    private bool _typeAll = false; //escirbe la linea completa
    private float _time;
    private InputAction _talk;
    private bool _talking;
    private int _index = 0;
    private bool _pressed;
    private bool _first;
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
    /// sistema de dialogo, usa un archivo de texto y lee el contenido y lo muestra linea por linea
    void Start()
    {
        Canvas.enabled = false;

        string ruta = Path.Combine(Application.streamingAssetsPath, Archivo);
        string rutacon = Path.Combine(Application.streamingAssetsPath, ArchivoCon);

        if (File.Exists(ruta))
        {
            string texto = File.ReadAllText(ruta); // lee el archivo
            _script = texto.Split('\n'); //lo separa por lineas
            Debug.Log(texto.Length);
        }
        else
        {
            Debug.LogError("No se encontró el archivo en: " + ruta);
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
        if (_talking)
        {   if (_line == _script.GetLength(0) + 1 || _line == _scriptCon.GetLength(0) + 1)
                {
                    Canvas.GetComponent<Canvas>().enabled = false;
                    _line = 0;
                    _talking = false;
                    _puzzle = false;
                    
                }//si avanza dialogo en la ultima linea regresa al estado de entrar
                 if (_puzzle&&Huye) {
                Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
                        _talking=true;
                    }
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
                        if (!_puzzle&&_line< _script.GetLength(0))
                        {
                            EscribirLinea(_script[_line]);
                        }
                        else if (_puzzle&&_line< _scriptCon.GetLength(0)) EscribirLinea(_scriptCon[_line]);
                        _line++;
                    }
            }
        }
        else
        {
          //  Canvas.GetComponent<Canvas>().enabled = false;
            _line = 0;
            _puzzle = false;
        }
        if (_type)
        {
            if (_typeAll)
            {
                dialogo.text = $"{Nombre}: {_currentScript}";
                _type = false;
                _typeAll = false;
                return;
            }
            else
            {
                _time += Time.deltaTime;

                if (_time >= Velocidad && _index < _currentScript.Length)
                {
                    dialogo.text += _currentScript[_index];
                    _index++;
                    _time = 0;
                }

                if (_index >= _currentScript.Length)
                {
                    _type = false;
                }
            }
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    public void puzzleSwitch(bool sel)
    {
        _puzzle = sel;
    }

    public bool istalking()
    {
        return _talking;
    }


    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.GetComponent<Player_Controller>() != null) { _talking = true; }
    //}
    public void talk()
    {
        if (!_talking)
        {
            _talking = true;
            _first = true;
            _currentScript = "";
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Test_detect_correction>() != null) { _talking = false; Canvas.enabled = false; _currentScript = ""; }
    }
    private void EscribirLinea(string linea) //animacion de escribir por letra, el nombre del personaje se queda afuera del bucle para que no cambie
    {
        _index = 0;
        _time = 0;
        _currentScript = linea;
        dialogo.text = $"{Nombre}: ";
        _type = true;
    }
}
    
    #endregion   

 // class Dialogo 
// namespace

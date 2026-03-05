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
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private InputAction _talk; //la accion
    private bool _talking; //indica la posibilidad de hablar
    private string [] _script; //las lineas de dialogo
    private string[] _scriptCon; //lineas del dialogo alternativo
    private int _line; //el indice para las lineas de dialogo
    private bool _puzzle; //activa el dialogo fuera del input
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
        Canvas.GetComponent<Canvas>().enabled = false;
        _talk = InputSystem.actions.FindAction("Interact");
        if (_talk == null )
        {
            Debug.Log("Error con la acción talk");
        }
       
        string ruta = Path.Combine(Application.streamingAssetsPath,Archivo);
        string rutacon = Path.Combine(Application.streamingAssetsPath,ArchivoCon);

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
        _line = 0;
        _puzzle = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_talking) 
        { 
            if (_line == _script.GetLength(0)+1|| _line == _scriptCon.GetLength(0) + 1) { Canvas.GetComponent<Canvas>().enabled = false; _line = 0; _puzzle = false; } //si avanza dialogo en la ultima linea regresa al estado de entrar
            if (_talk.ReadValue<float>() > 0.5f &&_talk.WasPressedThisFrame()) //detecta solo un frame de input o la llamada de otro componente
            {
                this.gameObject.GetComponent<Conditional_Test>().Check();
                Debug.Log(_puzzle);
                if (!_puzzle)
                {
                Canvas.GetComponent<Canvas>().enabled = true;
                if (_line < _script.GetLength(0)) MostrarLinea(_script);  //comprueba que estas dentro del array de dialogo
                _line++;
                }
                else
                {
                    Canvas.GetComponent<Canvas>().enabled = true;
                    if (_line < _scriptCon.GetLength(0)) MostrarLinea(_scriptCon);  //comprueba que estas dentro del array de dialogo
                    _line++;
                }
            }
            
         }
        else { Canvas.GetComponent<Canvas>().enabled = false; _line = 0;_puzzle = false; } //una vez se aleja el dialogo regresa al estado inicial
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
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player_Controller>() != null)
        {
            _talking = true; //indica que es posible empezar dialogo
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player_Controller>() != null)
        {
            _talking=false; //prohibe empezar o seguir dialogo
        }
    }
    private void MostrarLinea(string[] log)
    {
        StopAllCoroutines();
        StartCoroutine(EscribirLinea(log[_line]));
    }

    IEnumerator EscribirLinea(string linea) //animacion de escribir por letra, el nombre del personaje se queda afuera del bucle para que no cambie
    {
        dialogo.text = $"{Nombre}: ";

        foreach (char letra in linea)
        {
            dialogo.text += letra;
            yield return new WaitForSeconds(Velocidad);
        }
    }
}
    
    #endregion   

 // class Dialogo 
// namespace

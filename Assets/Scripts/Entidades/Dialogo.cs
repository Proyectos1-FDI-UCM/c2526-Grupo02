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
    private GameObject Canvas;
    [SerializeField]
    private Text dialogo;
    [SerializeField] 
    private GameObject Jugador;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints
    private InputAction _talk;
    private bool _talking;
    private bool _isTalking;
    private string _script;
    private int _line;
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
    void Start()
    {
        Canvas.SetActive(false);
        _talk = InputSystem.actions.FindAction("Talk");
        if (_talk == null )
        {
            Debug.Log("Error con la acción talk");
        }
        if (Jugador == null)
        {

            Debug.Log("No se encontró Jugador seleccionado");
            return;
        }
        _line = 0;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        _script = Guion(_line);
        
        if (_talking) 
        {
            if (_talk.ReadValue<float>() > 0.5f&&_talk.WasPressedThisFrame())
            {
                Canvas.SetActive(true);
                _line++;
                dialogo.text = $"Chamito: {_script}";
                _isTalking = true;
                if (_script == "")
                { _talking = false; Canvas.SetActive(false); /*Jugador.GetComponent<Player_Controller>().Resume();*/ _line = 0;_isTalking = false; }
            }
            if (_isTalking)
            {
               //Jugador.GetComponent<Player_Controller>().Stop();
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
            _talking = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player_Controller>() != null)
        {
            _talking=false;
        }
    }
   private string Guion (int n)
    {
        switch (n)
        {
            case 0: return "empanada"; break;
            case 1: return "bazinga"; break;
            case 2: return "buche"; break;
            default: return ""; break;
        }
       
    }
    #endregion   

} // class Dialogo 
// namespace

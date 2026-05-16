//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class DebugMenu : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    Canvas DebugCanvas;//El canvas con el menu que queremos activar;
    [SerializeField]
    GameObject FirstButton;//El primer boton que podemos elegir;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private InputAction _OpenDebug;
    private bool _menuState = false;

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
  
    void Start()
    {
        DebugCanvas.gameObject.SetActive(false);
        //Comprobamos que no sea nulo el canvas;
        if (DebugCanvas == null)
        {
            Debug.Log("No hay menu Debug");
        }
        //Asignamos la acción _OpenDebug y comprobamos que no sea nula
        _OpenDebug = InputSystem.actions.FindAction("Debug");
        if (_OpenDebug == null)
        {
            Debug.Log("No hay acción Debug");
        }
        if(FirstButton == null)
        {
            Debug.Log("configure el primer botón");
        }
    }
    void Update()
    {
        if (_OpenDebug.WasPressedThisFrame())
        {
            DebugCanvas.gameObject.SetActive(_menuState);
            if (_menuState)
            {
                SetFirstSelectedButton();
            }
            _menuState = !_menuState;
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
public void SetFirstSelectedButton()
    {
        EventSystem.current.SetSelectedGameObject(FirstButton);
    }
    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class DebugMenu 
// namespace

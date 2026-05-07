//---------------------------------------------------------
// Se encarga de manejar el menu de la pausa
// AlejandrA
// Don't go up
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
public class MenuController : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    private GameObject _menuCanvas; //Canvas general

    [SerializeField]
    private GameObject _panelControles; //Panel controles

    [SerializeField]
    private GameObject _ControlsExitButton;

    [SerializeField]
    private GameObject _FirstButton;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    //Atributo de la acción para menu
    private InputAction _pausa;
    private float _timer;
    private float _delayTime = 0.05f;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour


    void Start()
    {
        _pausa = InputSystem.actions.FindAction("Pausa");
        if (_pausa == null)
        {
            Debug.Log("No se ha encontrado la acción pausa");
            return;
        }
        _menuCanvas.SetActive(false);
        _panelControles.SetActive(false);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_pausa.WasPressedThisFrame())
        {
            if (_timer >= _delayTime)
            {
                _timer = 0;
                _panelControles.SetActive(false);
                _menuCanvas.SetActive(!_menuCanvas.activeSelf); //devuelve lo contrario a como entre
                Pausa_controller.SetPause(_menuCanvas.activeSelf);
                EventSystem.current.SetSelectedGameObject(_FirstButton);
            }
        }
        else
        {
            if (_timer < _delayTime)
            {
                _timer += Time.deltaTime;
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
    public void AbrirControles() 
    {
        _panelControles.SetActive(true);
        _menuCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_ControlsExitButton); 

    }
    public void CerrarControles()
    {
        _panelControles.SetActive(false);
        _menuCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_FirstButton);

    }
    /// <summary>
    /// Cierra el Canvas y ya
    /// </summary>
    public void CerrarCanvas()
    {
        _panelControles.SetActive(false);
        _menuCanvas.SetActive(false);
        Pausa_controller.SetPause(false);
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class MenuController 
  // namespace
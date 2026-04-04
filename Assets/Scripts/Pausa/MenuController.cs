//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Don't go up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
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
    private GameObject _menuCanvas;
    [SerializeField]
    private Button boton;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    //Atributo de la acción para menu
    private InputAction _pausa;
    //ref al jugador
    private GameObject _player;

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
        _player = GameManager.Instance.GetPlayer();
        _menuCanvas.SetActive(false);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_pausa.WasPressedThisFrame())
        {
            _menuCanvas.SetActive(!_menuCanvas.activeSelf); //devuelve lo contrario a como entre
            Pausa_controller.SetPause(_menuCanvas.activeSelf);

            if (Pausa_controller.IsGamePaused)
            {
                _player.GetComponent<Player_Controller>().Stop();
            }
            else
            {
                _player.GetComponent<Player_Controller>().Resume();
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

    public void CerrarCanvas()
    {
        _menuCanvas.SetActive(false);
        _player.GetComponent<Player_Controller>().Resume();
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
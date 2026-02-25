//---------------------------------------------------------
// Responsable de la creación de este archivo  JESUS DIEZ
// Nombre del juego  
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;


/// Gestiona la altura de la cámara en un juego 2D,
/// permite alternar entre altura normal y elevada con una tecla,
/// y suaviza la transición de altura mediante Lerp.

public class LookUp : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----

    [Header("Alturas de cámara")]
    [SerializeField] private float alturaNormal = 2f;
    [SerializeField] private float alturaElevada = 6f;

    [Header("Velocidad transición altura")]
    [SerializeField] private float velocidadTransicionAltura = 3f;

    [Header("Jugador")]
    [SerializeField] private GameObject Jugador;


    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados
    /// <summary>Indica si la cámara está en altura elevada</summary>
    private bool _alturaAlta = false;

    ///<summary>Controles
    private InputAction _lookUp;

    /// <summary> Variables para hacer un temporizador que frene el presionado de los botones
    private float _nextMov;


    /// </summary>
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos MonoBehaviour

    private void Start()
    {
        _lookUp = InputSystem.actions.FindAction("LookUP");
        if (Jugador == null)
        {
            
            Debug.Log("No se encontró Jugador seleccionado");
            return;
        }
        else if(Jugador.GetComponent<Player_Controller>() == null)
        {
            Debug.Log("El jugador no tiene el Player_Controller");
        }
        if (_lookUp == null)
        {
            Debug.Log("No se encontró la acción para mirar hacia arriba");
            return;
        }

    }

    private void Update()
    {
        // Leer valor de la acción (0 = no presionada, 1 = presionada)
        bool teclaActual = _lookUp.ReadValue<float>() > 0.5f;

        // Detecta solo el "press" (como GetKeyDown)
        if (teclaActual && _nextMov < Time.time)
        {
            _nextMov = Time.time + 1;
            _alturaAlta = !_alturaAlta;
        }
        Vector3 act = transform.position;
        float yObj = alturaNormal;

        if (_alturaAlta)
        {
            yObj = alturaElevada;
            Jugador.GetComponent<Player_Controller>().Stop();
        }
        else
        {
            yObj = alturaNormal;
            Jugador.GetComponent<Player_Controller>().Resume();
            
        }

        // Lerp suave hacia la altura objetivo
        act.y = Mathf.Lerp(
            act.y,
            yObj,
            velocidadTransicionAltura * Time.deltaTime
        );
        transform.position = act;
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos Públicos
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Aquí se pueden añadir métodos auxiliares privados si se necesitan
    #endregion
}

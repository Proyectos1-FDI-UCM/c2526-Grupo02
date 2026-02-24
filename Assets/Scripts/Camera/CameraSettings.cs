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

public class CameraSettings : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    [Header("Velocidad seguimiento")]
    [SerializeField] private float velocidad = 5f;

    [Header("Alturas de cámara")]
    [SerializeField] private float alturaNormal = 2f;
    [SerializeField] private float alturaElevada = 6f;

    [Header("Velocidad transición altura")]
    [SerializeField] private float velocidadTransicionAltura = 3f;

    [Header("Input System")]
    [SerializeField] private InputActionReference teclaAltura = null;

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados
    /// <summary>Indica si la cámara está en altura elevada</summary>
    private bool _alturaAlta = false;

    /// <summary>Controla el estado anterior de la tecla para detectar pulsación</summary>
    private bool _teclaAnterior = false;

    /// <summary>Altura actual de la cámara (Lerp suave)</summary>
    private float _alturaActual;
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos MonoBehaviour

    private void Start()
    {
        _alturaActual = alturaNormal;
    }

    private void Update()
    {
        if (teclaAltura == null) return;

        // Leer valor de la acción (0 = no presionada, 1 = presionada)
        bool teclaActual = teclaAltura.action.ReadValue<float>() > 0.5f;

        // Detecta solo el "press" (como GetKeyDown)
        if (teclaActual && !_teclaAnterior)
            _alturaAlta = !_alturaAlta;

        _teclaAnterior = teclaActual;

        // Lerp suave hacia la altura objetivo
        float alturaObjetivo = _alturaAlta ? alturaElevada : alturaNormal;
        _alturaActual = Mathf.Lerp(
            _alturaActual,
            alturaObjetivo,
            velocidadTransicionAltura * Time.deltaTime
        );
    }

    private void OnEnable()
    {
        teclaAltura?.action.Enable();
    }

    private void OnDisable()
    {
        teclaAltura?.action.Disable();
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos Públicos
    /// <summary>Devuelve la altura actual de la cámara (para CameraFollow)</summary>
    public float GetAlturaActual()
    {
        return _alturaActual;
    }

    /// <summary>Devuelve la velocidad de seguimiento de la cámara</summary>
    public float GetVelocidad()
    {
        return velocidad;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Aquí se pueden añadir métodos auxiliares privados si se necesitan
    #endregion
}

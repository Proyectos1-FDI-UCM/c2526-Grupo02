//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Lamp : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    public float _angles = 45.0f; // Qué tan lejos llega (en grados)
    [SerializeField]
    public float _speed = 2.0f;
    [SerializeField]
    private GameObject visualPanel; // Panel que señaliza la visión del enemigo
    [SerializeField]
    private Collider2D visionCollider; //Collider aparte que representa el campo visual del enemigo.// Qué tan rápido oscila

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private Enemy_Detect _enemyDetect;  // llamo al _enemyDetect que tiene el propio enemigo
    private Phases Phase;

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
    /// 


    void Awake()
    {
        Phase = GetComponent<Phases>();
        if (Phase == null)
        {
            Debug.Log("No esta el script phase en el enemigo");
        }
        Phase.SetVisualPanel(visualPanel);
        _enemyDetect = visualPanel.GetComponent<Enemy_Detect>();
        if (_enemyDetect == null)
        {
            Debug.Log("No hay enemyDetect en el panelvisual");
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!Pausa_controller.IsGamePaused)
        {
            float angle = _angles * Mathf.Sin(Time.time * _speed);

            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
    private void OnTriggerStay2D(Collider2D collision) // Mientras el jugador este dentro del enemigo se irá comprobando en que fase se encuentra
    {
        Phase.EnemyPhases(collision);
    }
    #endregion

} // class Lamp 
// namespace

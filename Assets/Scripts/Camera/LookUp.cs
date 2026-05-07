//---------------------------------------------------------
// Responsable de la creación de este archivo  JESUS DIEZ
// Nombre del juego - Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Gestiona la altura de la cámara,
/// permite alternar entre altura normal y elevada con una tecla,
/// y suaviza la transición de altura mediante Lerp.
/// </summary>

public class LookUp : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----

    [Header("Alturas de cámara")]
    // Altura estándar de la cámara
    [SerializeField] private float alturaNormal = 2f;
    // Altura elevada al presionar la tecla
    [SerializeField] private float alturaElevada = 6f;

    // Velocidad de interpolación entre alturas
    [Header("Velocidad transición altura")]
    [SerializeField] private float velocidadTransicionAltura = 3f;

    //Referencia al jugador, necesario para posicionar la cámara
    [Header("Jugador")]
    [SerializeField] private GameObject Jugador;

    [Header("Configuración")]
    [SerializeField]
    bool CanLookUp;


    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados
    // Indica si la cámara está en altura elevada
    private bool _alturaAlta = false;
    // Objeto de interacción
    private GameObject interactionCollider;

    // Acción de input para mirar hacia arriba
    private InputAction _lookUp;

    // Temporizador para controlar la frecuencia de cambio de altura
    private float _nextMov;


    //
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos MonoBehaviour

    private void Start()
    {
        // Buscar la acción "LookUP" definida en el Input System
        // Aviso de falta de asignar al jugador en el inspector
        // o que la falta el componente Player_Controller
        //o que falta de asignar la acción "mirar arriba"
        //Programación defensiva
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
        interactionCollider = Jugador.GetComponentInChildren<Test_detect_correction>().gameObject;

    }

    private void Update()
    {
        if (!Pausa_controller.IsGamePaused)
        {
            if (CanLookUp)
            {
                // Leer valor de la acción (0 = no presionada, 1 = presionada)
                bool teclaActual = _lookUp.ReadValue<float>() > 0.5f;

                //  Detecta solo la pulsación inicial, evitando que se active continuamente
                if (teclaActual && _nextMov < Time.time && !Pausa_controller.IsGamePaused)
                {
                    // Se añade un retraso de 1 segundo antes de permitir otra acción
                    _nextMov = Time.time + 1;
                    // Alterna entre altura normal y elevada
                    _alturaAlta = !_alturaAlta;
                    if (_alturaAlta)
                    {
                        Jugador.GetComponent<Player_Controller>().Stop();
                    }
                    else
                    {
                        Jugador.GetComponent<Player_Controller>().Resume();
                    }

                }
                Vector3 act = transform.position;// Posición actual de la cámara
                float yObj = alturaNormal; // Altura objetivo por defecto
                Vector3 interactpos = interactionCollider.transform.position;

                if (_alturaAlta)
                {
                    // Ajuste de altura sobre el jugador
                    yObj = alturaElevada + Jugador.transform.position.y;

                    //El stop Si se pone en el update porque otro script podría llegar a permitir al jugador moverse pero por este no se debería poder.
                    Jugador.GetComponent<Player_Controller>().Stop();
                }
                else
                {
                    // Altura normal sobre el jugador
                    yObj = alturaNormal + Jugador.transform.position.y;
                }

                // Interpolación suave (Lerp) hacia la altura objetivo
                act.y = Mathf.Lerp(
                    act.y,
                    yObj,
                    velocidadTransicionAltura * Time.deltaTime
                );
                // Actualiza la posición de la cámara
                transform.position = act;
                interactpos.y = yObj;
                interactionCollider.transform.position = interactpos;
            }
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos Públicos
    // Permite consultar desde otras clases si la cámara está elevada
    public bool GetAlturaAlta() { return _alturaAlta; }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Aquí se pueden añadir métodos auxiliares privados si se necesitan
    #endregion
}

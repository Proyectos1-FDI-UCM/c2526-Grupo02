//---------------------------------------------------------
// Responsable de la creación de este archivo  JESUS DIEZ
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using static UnityEditor.SceneView;

/// <summary>
/// Script encargado de seguir al jugador en un juego 2D,
/// utilizando la altura proporcionada por CameraSettings
/// y suavizando el movimiento con Lerp.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector
    [SerializeField] private Transform player = null;
    [SerializeField] private CameraSettings cameraSettings = null;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    #endregion

    //Atributos privados.

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
  
 
    private void Start()
    {
        if (cameraSettings == null)
            Debug.LogWarning($"CameraSettings no asignado en {name}. Arrastra el componente al inspector.", this);

        if (player == null)
            Debug.LogWarning($"Player no asignado en {name}. Arrastra el jugador al inspector.", this);
    }

    /// LateUpdate se llama después de todos los Update para
    /// que la cámara siga al jugador suavemente.
    ///
    /// 
    private void LateUpdate()
    {
        if (player == null || cameraSettings == null) return;

        // Construimos la posición objetivo con la altura actual de CameraSettings
        Vector3 targetPosition = new Vector3(
            player.position.x + offset.x, //ESTA LINEA ES LA QUE HACE QUE SIGUA EL JUGADOR EN X
            cameraSettings.GetAlturaActual() + offset.y,
            offset.z
        );

        // Lerp para suavizar el movimiento de la cámara
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            cameraSettings.GetVelocidad() * Time.deltaTime
        );
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos Públicos
    // Se pueden añadir métodos públicos si se necesitan
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Se pueden añadir métodos privados auxiliares
    #endregion
}

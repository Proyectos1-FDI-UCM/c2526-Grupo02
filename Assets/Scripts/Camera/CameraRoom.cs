//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo  JESUS DIEZ
// Nombre del juego - Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.EventSystems.EventTrigger;

/// <summary>
/// Controla el movimiento de la cámara en un sistema 2D por habitaciones.
/// Evita comportamientos erráticos al cambiar de sala.
/// Limita la posición de la cámara dentro de los límites de cada habitación
/// definidos por colliders.
/// Los límites se ajustan automáticamente en función del tamaño visible de la cámara.
/// Se actualizan cuando el jugador entra en una nueva habitación mediante RoomTrigger.
/// </summary>
public class CameraRoom : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    // Límites de movimiento de la cámara
    private float minX, maxX, minY, maxY;

    // Referencia a la cámara (para calcular tamaño visible)
    private Camera _cam;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
 
    void Start()
    {
        // Obtiene la referencia a la cámara del GameObject
        _cam = GetComponent<Camera>();
    }

    /// <summary>
    /// Se usa LateUpdate para mover la cámara después del jugador
    /// </summary>
    private void LateUpdate()
    {
        // Obtiene la posición actual de la cámara
        Vector3 pos = transform.position;

        // Limita la posición dentro de los límites de la habitación
        //los bounds son el collider de la habitación menos la mitad del ancho de la visión de la cámara

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Aplicamos la posición final
        transform.position = pos;
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    /// <summary>
    /// Establece los límites de movimiento de la cámara.
    /// Este método es llamado desde RoomTrigger cuando el jugador entra en una nueva habitación
    /// Ajusta los límites teniendo en cuenta el tamaño visible de la cámara
    /// para evitar mostrar zonas fuera del escenario.
    /// </summary>
    public void SetBounds(float _minX, float _maxX,float _minY,float _maxY)
    {
        // Tamaño vertical del visor de la cámara ortográfica  
        // de Unity,
        //si ponemos este  de la cámara, no necesitaremos
        //ajustarlo cada vez que cambiamos el tamaño del visor de la cámara
        //este se adapta automáticamente.

        float vertExtent = _cam.orthographicSize;

        // Tamaño horizontal visible según la resolución de pantalla
        //Screem.width son los campos(atributos) de la pantalla,
        //para que se adapten automáticamente
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Ajusta los límites para que la cámara no muestre zonas fuera de la habitación
        // fuera de la habitación
        //horzExtent, mitad del ancho visible...
        //minX... límites e la habitación, las obtiene el script
        //RoomTrigger y las envía a este script
        minX = _minX + horzExtent;
        maxX = _maxX - horzExtent;
        minY = _minY + vertExtent;
        maxY = _maxY - vertExtent;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion

} // class CameraRoom 
// namespace

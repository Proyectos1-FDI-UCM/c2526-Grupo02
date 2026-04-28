//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo  JESUS DIEZ
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEditor.PlayerSettings;



/// <summary>
//Corrige el comportamiento errático de la cámara al pasar de habitaciones
//del antiguo sistema que usábamos basado en raycasts
//Este script controla el movimiento de la cámara en un sistema 2D por habitaciones.
// Su función es limitar la posición de la cámara dentro de unos límites (BOUNDS), se usa el campo bounds de collider ("caja física")
//Bounds es la caja con sus limites referenciados al "mundo"
//IMPORTANTE: los limites  son el collider de la habitación menos la mitad del ancho de la visión de la cámara. IMPORTANTE
//que se actualizan automática cuando el jugador entra en una nueva habitación que dispone de un 
//collider(trigger) que se ajusta a los límites de la habitación y que lleva el script RoomTrigger. 
//RoomTrigger  informa automáticamente a este script de los nuevos límites de las paredes de
//cada vez que entre en otra habitación

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
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        // Obtenemos el componente Camera del mismo GameObject
        _cam = GetComponent<Camera>();
    }

    /// <summary>
    //
    //La posición de la cámara la actualizamos en el lateupdate
    /// </summary>
    private void LateUpdate()
    {
        // Posición actual de la cámara
        Vector3 pos = transform.position;

        // Limitamos la posición dentro de los bounds
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
    // Co este método, llamado desde el script RoomTrigger colocado en los
    // collider marcados como trigger en las habitaciones,
    // se actualizan los limites de los collider de la habitación y se ajustan a el tamaño
    // donde se mueve el jugador teniendo en cuenta el tamaño de la cámara.
    // </summary>
    public void SetBounds(float _minX, float _maxX, float _minY, float _maxY)
    {
        // Tamaño vertical del visor de la cámara (ortográfica)  
        // de Unity,
        //si ponemos este  de la cámara, no necesitaremos
        //ajustarlo cada vez que cambiamos el tamaño del visro de la cámara
        //este se adapta automáticamente.

        float vertExtent = _cam.orthographicSize;

        // Tamaño horizontal calculado según la resolución
        //Screem.width son los campos(atributos) de la pantalla,
        //para que se adapten automáticamente
        float horzExtent = vertExtent * Screen.width / Screen.height;

        // Ajustamos los límites para que la vista de la cámara no muestra nada
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
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class CameraRoom 
// namespace

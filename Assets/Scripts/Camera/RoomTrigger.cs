//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo  JESUS DIEZ
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;


/// <summary>
/// Este script se coloca en boxcollider2D que va sobre el sprite que
/// de cada habitación. El colladier tiene que tener la mismo forma y tamaño.
/// El boxcolladier debe tener el mismo nombre que la habitación 
/// ej. cocina -> RoomTrigger_cocina (se indica por programación defensiva,
/// Detecta cuando el jugador entra y envía a la cámara
/// los límites del collider (que están en el campo collider.bounds), 
/// y por tanto de la habitación
/// </summary>
public class RoomTrigger : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
 
    // Collider de la habitación (define su tamaño en el mundo)
    private BoxCollider2D _collider;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
   

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        // Obtenemos el BoxCollider2D del objeto
        _collider = GetComponent<BoxCollider2D>();

        // Si no hay collider, avisamos por consola
        if (_collider == null)
        {
            Debug.LogError("RoomTrigger necesita un BoxCollider2D en el mismo objeto.");
        }
    }

    /// <summary>
    /// Se ejecuta cuando algo entra en el trigger.
    /// Aquí detectamos si es el jugador.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Es por seguridad....
        //if (!other.CompareTag("Player")) return;
        // Buscamos el script del jugador (sin usar tags)
        Player_Controller player = other.GetComponent<Player_Controller>();
        
        // Solo si es el jugador que seguimos
        if (player != null)
        {
            // Buscamos la cámara principal (main) en la escena, el objeto que lleva la clase(sript) CamareraRoom localizando el objeto
            //Camera.main es la camara principal de la escena
            CameraRoom cam = Camera.main.GetComponent<CameraRoom>();

            if (cam != null)
            {
                // Obtenemos los límites del collider de la habitación
                //Bounds es un tipo de Unity, una caja que tiene limites
                Bounds bounds = _collider.bounds;

                // Enviamos los límites a la cámara (al Scrit CameraRoom
                // en el modificamos la Propiedad (SetBounds),
                // esto permite actualizar automáticamente los limites
                // de la habitación dond esta el jugador. 
                cam.SetBounds(
                    bounds.min.x,
                    bounds.max.x,
                    bounds.min.y,
                    bounds.max.y
                );
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

       #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion   
}

// class NewMonoBehaviourScript 
// namespace

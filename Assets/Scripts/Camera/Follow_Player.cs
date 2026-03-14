//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo ALEJANDRO, SARA, JESUS
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Follow_Player : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    
    // Transform del jugador que la cámara debe seguir
    [SerializeField]
    private Transform Target;
    //[SerializeField]
    //private float TargetEyes = 0.5f;
    
    // Velocidad de suavizado del movimiento de la cámara
    [SerializeField]
    private float SpringFactor = 5f;

    // Distancia a la que el raycast detecta paredes
    [SerializeField]
    private float DistanceToWall = 8.5f;

    // Altura desde la que se lanza el raycast
    [SerializeField]
    private float rayHeight = 3f;
    //[SerializeField]
    //private float rotationspringFactor = 5f;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    // Distancia mínima del raycast para considerar colisión
    private float _minRayDist = 0.00f;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    void Start()
    {
        // Comprobamos que el jugador está asignado
        if (Target == null)
        {
            Debug.Log("No has asignado ningún target a la cámara");
            return;
        }
    }
    private void Update()
    {
        // Posición actual del jugador
        Vector3 playerAct = Target.transform.position;

        // Posición desde la que se lanza el raycast
        Vector3 rayPos = Target.transform.position;
        rayPos.y += rayHeight;

        // Posición actual de la cámara
        Vector3 targetPos = transform.position;

        // Raycast a derecha e izquierda para detectar paredes
        bool _collidingR = Physics2D.Raycast(rayPos, Vector2.right, DistanceToWall).distance > _minRayDist;
        bool _collidingL = Physics2D.Raycast(rayPos, Vector2.left, DistanceToWall).distance > _minRayDist;


        // Si no hay colisiones, la cámara sigue al jugador suavemente
        if (!_collidingR && !_collidingL)
        {
            targetPos.x = Mathf.Lerp(targetPos.x, playerAct.x, (SpringFactor * Time.deltaTime));
            transform.position = targetPos;
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

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class Follow_Player 
// namespace

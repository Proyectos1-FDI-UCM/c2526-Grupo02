//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
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
    [SerializeField]
    private Transform Target;
    //[SerializeField]
    //private float TargetEyes = 0.5f;
    [SerializeField]
    private float SpringFactor = 5f;
    [SerializeField]
    private float DistanceToWall = 8.5f;
    [SerializeField]
    private float rayHeight = 3f;
    //[SerializeField]
    //private float rotationspringFactor = 5f;


    //private Vector3 offset; //Distancia entre punto A y B. (El juagdor y la cámara)
    //private Vector3 currentPos; // Posicion actual con el movimiento 'suave' de la cámara
    //private Vector3 expectedPos;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    void Start()
    {
        if (Target == null)
        {
            Debug.Log("No has asignado ningún target a la cámara");
            return;
        }
    }
    private void Update()
    {
        Vector3 playerAct = Target.transform.position;
        Vector3 rayPos = Target.transform.position;
        rayPos.y += rayHeight;
        Vector3 targetPos = transform.position;
        //expectedPos = Target.position + offset; //Posicion ideal con el objetivo y el offset. 
        //Vector3 act = transform.position;
        ////Vector3 targetEyes = Target.position + Vector3.up * TargetEyes;
        //Vector3 highPos = expectedPos + Vector3.up * 5f; 
        bool _collidingR = Physics2D.Raycast(rayPos, Vector2.right, DistanceToWall);
        bool _collidingL = Physics2D.Raycast(rayPos, Vector2.left, DistanceToWall);
        Debug.Log(_collidingL + "L " + _collidingR);
        if (!_collidingR && !_collidingL)
        {
            targetPos.x = Mathf.Lerp(targetPos.x, playerAct.x, (SpringFactor * Time.deltaTime));
            transform.position = targetPos;
        }
       

        /* Vector3 dir = (targetEyes - transform.position).normalized; *///Calculamos la dirección hacia los ojos
                                                                         //Quaternion lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
                                                                         //transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, rotationspringFactor * Time.deltaTime);
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

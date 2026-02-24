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
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private float TargetEyes = 0.5f;
    [SerializeField]
    private float springFactor = 5f;
    [SerializeField]
    private float rotationspringFactor = 5f;
    

    private Vector3 offset; //Distancia entre punto A y B. (El juagdor y la cámara)
    private Vector3 currentPos; // Posicion actual con el movimiento 'suave' de la cámara
    private Vector3 expectedPos;

    void Start()
    {
        offset = transform.position - Target.position; // Calculamos la distancia inicial.
    }
    private void LateUpdate()
    {
        if (Target == null) return;

        expectedPos = Target.position + offset; //Posicion ideal con el objetivo y el offset. 
        Vector3 bestPos = expectedPos;
        Vector3 targetEyes = Target.position + Vector3.up * TargetEyes;
        Vector3 highPos = expectedPos + Vector3.up * 5f; 
        currentPos = Vector3.Lerp(transform.position, bestPos, (springFactor * Time.deltaTime));
        transform.position = currentPos;

        Vector3 dir = (targetEyes - transform.position).normalized; //Calculamos la dirección hacia los ojos
        Quaternion lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, rotationspringFactor * Time.deltaTime);


        #region Atributos del Inspector (serialized fields)
        // Documentar cada atributo que aparece aquí.
        // El convenio de nombres de Unity recomienda que los atributos
        // públicos y de inspector se nombren en formato PascalCase
        // (palabras con primera letra mayúscula, incluida la primera letra)
        // Ejemplo: MaxHealthPoints

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

        /// <summary>
        /// Start is called on the frame when a script is enabled just before 
        /// any of the Update methods are called the first time.
        /// </summary>
        void Start()
    {
        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
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

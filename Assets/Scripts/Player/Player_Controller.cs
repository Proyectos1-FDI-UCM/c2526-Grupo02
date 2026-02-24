//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Player_Controller : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    //Atributo que nos dice la velocidad máxima del jugador.
    [SerializeField]
    int Speed;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private InputAction _move;
    private Rigidbody2D _rb;

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
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null )
        {
            Debug.Log("No hay Rigidbody");
            return;
        }
        _move = InputSystem.actions.FindAction("move");
        if(_move == null)
        {
            Debug.Log("No se ha encontrado la acción move");
            return;
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Calculamos la dirección del movimiento y se la sumamos a la posición x multiplicandolo por la velocidad y el time.deltatime
      Vector2 dir = _move.ReadValue<Vector2>();
        float HorizontalDir = Mathf.Round(dir.x);
        Quaternion rot = transform.rotation;
        Vector2 pos = transform.position;

        //Redondeamos el valor de dir.x para que en todas las plataformas y controladores el movimiento sea igual.
            pos.x += HorizontalDir * Speed * Time.deltaTime;
            transform.position = pos;

        //Calculamos la dirección a la que mira el jugador
        Debug.Log(HorizontalDir);
        rot.x = 0;
        rot.z = 0;
            if(HorizontalDir == -1)
            {
               rot.y = 180;
            }
            else if(HorizontalDir == 1)
            {
               rot.y = 0;
            }
        transform.rotation = rot;


        
       
        
        
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

} // class Player_Controller 
// namespace

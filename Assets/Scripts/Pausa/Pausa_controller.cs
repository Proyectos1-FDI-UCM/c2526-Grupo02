//---------------------------------------------------------
// Maneja la pausa, es decir, cuando se pausa el juego se encarga de manejar eso
// AlejandrA
// Don't go up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Pausa_controller : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
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

    #endregion


    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    /// <summary>
    ///  Se puede mirar si el juego está en pausa desde cualquier sitio,
    ///  pero solo aquí se puede cambiar el valor
    /// </summary>
    public static bool IsGamePaused { get; private set; }

    /// <summary>
    /// Cambia el estado de pausa del juego
    /// </summary>
    public static void SetPause(bool paused)
    {
        IsGamePaused = paused;

        //Busca todos los objetos activos en la escena que tengan el componente Enemy_Detect,
        //no los ordena y los guarda en el array enemigos
        Enemy_Detect[] enemigos = Object.FindObjectsByType<Enemy_Detect>(FindObjectsSortMode.None);

        //
        foreach (Enemy_Detect enemy in enemigos) //por cada enemigo dentro del array enemigos 
        {
            enemy.gameObject.SetActive(!paused); //Desactiva a los enemigos, para que no te puedan atacar
        }

        if (IsGamePaused) //si está pausao el jugador no se mueve
        {
            GameManager.Instance.GetPlayer().GetComponent<Player_Controller>().Stop();
        }
        else
        {
            GameManager.Instance.GetPlayer().GetComponent<Player_Controller>().Resume();
        }
    }


    #endregion


    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class Pausa_controller 
// namespace

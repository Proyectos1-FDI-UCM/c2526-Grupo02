//---------------------------------------------------------
// Script que lleva las fases de los enemigos
// Responsable de la creación de este archivo - Pablo
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEditor;
using UnityEngine;
using static Flags;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Phases : MonoBehaviour
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
    private GameObject _visualPanel; //Panel que señaliza la visión del enemigo
    private Enemy_Detect _detect;  // llamo al detect
    private Color _white = new Color(1, 1, 1, 0.5f);
    private Color _yellow = new Color(1, 0.92f, 0.016f, 0.5f);
    private Color _red = new Color(1, 0, 0, 0.5f);

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    private void Start()
    {
        
        _detect = _visualPanel.GetComponent<Enemy_Detect>(); // llamo a los componentes que me harán falta más adelante
        if (_detect == null)
        {
            Destroy(this);
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

    public void SetVisualPanel(GameObject panel)
    {
        _visualPanel = panel;
    }

    public void EnemyPhases(Collider2D collision) //Método que lleva las fases de los enemigos
    {
        if (!Pausa_controller.IsGamePaused)
        {
            var player = collision.GetComponent<Player_Controller>();

            if (player != null)
            {
                int fase = _detect.GetState();
                switch (fase)
                {
                    case 0:
                        _visualPanel.GetComponent<SpriteRenderer>().color = _white;
                        break;
                    case 1:
                        _visualPanel.GetComponent<SpriteRenderer>().color = _yellow;
                        break;
                    case 2:
                        _visualPanel.GetComponent<SpriteRenderer>().color = _red;
                        break;
                    case 3:
                        GameManager.Instance.GameOver();
                        break;
                }
            }
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

} // class Phases 
// namespace

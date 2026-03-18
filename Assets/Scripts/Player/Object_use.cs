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
public class Object_use : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints

    [SerializeField]
    private UnityEngine.UI.Image Mano;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    // Objeto selecionado por el jugador
    private Object ObjetoEnUso;

    private Sprite _emptyHand;
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

    private void Start()
    {
        if (Mano == null)
        {
            Debug.Log("No hay mano configurada");
        }
        _emptyHand = Mano.sprite;
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    // Método que señala al jugador que objeto a recogido 
    public void ObjetoRecogido(Object objeto)
    {
        Debug.Log("Estoy usando " + objeto.GetItem().ToString());
        if (ObjetoEnUso == objeto)
        {
            //hemos pasado esto a un método auxiliar ya que lo necesitamos para cuando usamos el objeto y nos lo quitan del inventario
            RemoveFromHand();

        }
        else
        {
            ObjetoEnUso = objeto;
            Mano.sprite = objeto.GetInventorySprite();
        }

    }

    public Object GetHandItem()
    {
        return ObjetoEnUso;
    }
    public void RemoveFromHand()
    {
        ObjetoEnUso = null;
        Mano.sprite = _emptyHand;
    }
        
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion

} // class Object_use 
// namespace

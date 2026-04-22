//---------------------------------------------------------
// Script que nos permite llamar a funciones (como el interactuable) automáticamente al chocar con un trigger
// Alejandro Jiméenz Rojo
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;
// Añadir aquí el resto de directivas using


/// <summary>
/// Clase en la que tenemos una interacción automática que nos permite realizar diálogos automáticos, animaciones, etc, cuando el jugador colisiona con cierto punto.
/// </summary>
public class AutomaticInteraction : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    UnityEvent Code;//Métodos que vamos a ejecutar al interactuar con esto.

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    private bool _active = true;//atributo que nos dice si este trigger sigue activo o no

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    private void Start()
    {
        if(this.GetComponent<Collider2D>() == null)
        {
            Debug.Log("Falta un trigger");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_active)
        {
            Code.Invoke();
        }
       
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos Públicos
    // Permite cambiar el estado de _active
    public void SetActive(bool active)
    { _active = active; }
    #endregion

} // class AutomaticInteraction 
// namespace

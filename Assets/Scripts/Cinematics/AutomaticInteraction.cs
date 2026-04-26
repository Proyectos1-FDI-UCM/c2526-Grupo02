//---------------------------------------------------------
// Script que nos permite llamar a funciones (como el interactuable) automáticamente al chocar con un trigger
// Alejandro Jiméenz Rojo
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

public class AutomaticInteraction : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    UnityEvent Code;//Métodos que vamos a ejecutar al interactuar con esto.

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private bool _active = true;//atributo que nos dice si este trigger sigue activo o no

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    private void Start()
    {
        //Programación defensiva
        if(this.GetComponent<Collider2D>() == null)
        {
            Debug.Log("Falta un trigger");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SI entramos en el trigger se activan los métodos que asignemos a CODE
        //_active nos permite que no se vuelva a activar una vez activado (es decir, podemso desactivar _active en Code, para que no se vuelva a repetir)
        if(_active)
        {
            Code.Invoke();
        }
       
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos Públicos
    // Permite cambiar el estado de _active (ha sido usado o no)
    public void SetActive(bool active)
    { _active = active; }
    #endregion

} // class AutomaticInteraction 
// namespace

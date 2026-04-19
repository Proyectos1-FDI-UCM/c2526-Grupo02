//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo -Hermes
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class SaveState : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player_Controller>()!=null)
        {
            GameManager.Instance.SavePos(collision.gameObject.transform.position);
        for (int i = 0; i < GameManager.Instance.GetInv().gameObject.GetComponent<Inventory_Manager>().IniState(); i++)
        {
            GameManager.Instance.SaveInv(i);
        }
        for (int i = 0; i <  GameManager.Instance.GetFlags().GetFlagLenght(); i++)
        {
                GameManager.Instance.SavePuz(i);
        }
        Destroy(this.GetComponent<Collider2D>());
        }
    }

    #endregion   

} // class SaveState 
// namespace

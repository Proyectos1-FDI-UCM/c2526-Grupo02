//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo ALEJANDRO, SARA, JESUS
//JESUS (lo adapta finalmente para usarse con la corrección de la cámara errática)
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;



/// <summary>
/// Controla el movimiento de la cámara respecto al jugador,
/// Hacer que la cámara le siga.
/// Tiene un método público que teletransporta al jugador
/// a una nueva posición.
/// </summary>
public class Follow_Player : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    
    // Transform del jugador que la cámara debe seguir
    [SerializeField]
    private Transform Target;
 
    // Velocidad de suavizado del movimiento de la cámara
    [SerializeField]
    private float SpringFactor = 5f;

    

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Start()
    {
        // Comprobamos que el jugador está asignado en el inspector
        if (Target == null)
        {
            Debug.Log("No has asignado ningún target a la cámara");
            return;
        }
    }
    private void Update()
    {

       //La camara siga el jugador
        ///de forma suave
        ///Paro al jugador si el script Pausa_controller informa que el jugador esta 
        ///parado es falso
        ///!Pausa_controller.IsGamePaused
        
        if (!Pausa_controller.IsGamePaused)
        {
            Vector3 playerAct = Target.transform.position;
            Vector3 targetPos = transform.position;

            targetPos.x = Mathf.Lerp(targetPos.x, playerAct.x, SpringFactor * Time.deltaTime);
            targetPos.y = Mathf.Lerp(targetPos.y, playerAct.y, SpringFactor * Time.deltaTime);

            transform.position = targetPos;
        }

    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Teletransporte del jugador 
    public void Teleport(Vector3 pos)
    { 
        this.GetComponent<Transform>().position = pos;
    }
    #endregion

} // class Follow_Player 
// namespace

//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo ALEJANDRO, SARA, JESUS
// JESUS lo adaptó para usarse con la corrección de la cámara errática
// Nombre del juego - Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;

/// <summary>
/// Controla el seguimiento de la cámara respecto al jugador.
/// La cámara sigue al jugador de forma suave utilizando interpolación.
/// Incluye un sistema de suavizado para evitar movimientos bruscos.
/// También permite teletransportar al jugador a una posición concreta
/// </summary>
public class Follow_Player : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    // Referencia al jugador que la cámara debe seguir
    [SerializeField]
    private Transform Target;
 
    // Factor de suavizado del movimiento de la cámara
    [SerializeField]
    private float SpringFactor = 5f;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Start()
    {
        // Verifica que el jugador ha sido asignado en el Inspector
        if (Target == null)
        {
            Debug.Log("No se ha asignado un target a la cámara");
            return;
        }
    }
    private void Update()
    {
        // Hace que la cámara siga al jugador de forma suave.
        // El movimiento solo se ejecuta si el juego no está en pausa.

        // Solo actualiza la cámara si el juego no está pausado
        // según el estado de Pausa_controller
        if (!Pausa_controller.IsGamePaused)
        {
            Vector3 playerAct = Target.transform.position;
            Vector3 targetPos = transform.position;
            // Interpola suavemente la posición de la cámara hacia el jugador
            targetPos.x = Mathf.Lerp(targetPos.x, playerAct.x, SpringFactor * Time.deltaTime);
            targetPos.y = Mathf.Lerp(targetPos.y, playerAct.y, SpringFactor * Time.deltaTime);

            transform.position = targetPos;
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Teletransporta el objeto a una posición específica en el mundo
    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
    }
    #endregion

} // class Follow_Player 
// namespace

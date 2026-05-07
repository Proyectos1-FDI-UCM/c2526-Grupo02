//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo JESUS DIEZ
// Nombre del juego - Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
/// <summary>
/// </summary> para activar el cronómetro
///El cronómetro se activa al pasar el jugador por un collider colocado
///en un objeto
/// <summary>
public class TimerTrigger : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Referencia al script del temporizador que se va a activar
    [SerializeField] private TimeSlider timer;

    // Evita que el trigger se ejecute más de una vez
    [SerializeField] private bool onlyOnce = true;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Evita que el trigger se ejecute más de una vez
    private bool triggered = false;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto es el jugador y si el trigger puede ejecutarse
        if (other.CompareTag("Player") && (!onlyOnce || !triggered))
        {
            // Evita que el jugador pueda reiniciar el temporizador múltiples veces
            triggered = true;

            // Inicia el temporizador si está asignado
            if (timer != null)
                timer.StartTimer();
            // Mensaje de depuración para comprobar activación del trigger
            Debug.Log("Trigger activado: temporizador arrancado");
        }
    }


    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos


    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados


    #endregion   

} // class TimerTrigger 
// namespace

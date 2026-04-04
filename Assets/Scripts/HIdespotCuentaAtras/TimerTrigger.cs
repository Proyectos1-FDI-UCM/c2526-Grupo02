//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo JESUS DIEZ
//Nombre del juego - Dont go up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


//Script para poder probar el cronómetro
//El cronómetro se activa al pasar el jugador por un collader colocado
//en un objeto
public class TimerTrigger : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Referencia a tu temporizador
    [SerializeField] private TimeSlider timer;

    // Activar solo la primera vez
    [SerializeField] private bool onlyOnce = true;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    private bool triggered = false;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos que sea el jugador (tag "Player") y que no se haya activado ya
        if (other.CompareTag("Player") && (!onlyOnce || !triggered))
        {
            triggered = true;

            // Llama a StartTimer() del TimeSlider
            if (timer != null)
                timer.StartTimer();

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

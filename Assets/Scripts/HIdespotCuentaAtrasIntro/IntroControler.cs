//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo  JESUS DIEZ
// Nombre del juego - Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using UnityEngine;
// Directiva de la IU
using UnityEngine.UI;
// Directiva textos TMP
using TMPro;
using UnityEngine.InputSystem;

/// <summary>
///Controla la escena que narra
///la historia del niño y sus objetos del juego
/// </summary>
public class IntroControler : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    
    // Referencia al texto de la UI y el lugar se mostrará la historia
    [SerializeField] private TextMeshProUGUI textoUI;

    // Tiempo (en segundos) que se muestra cada frase
    [SerializeField] private float tiempoEntreFrases = 3f;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Array que contiene todas las frases de la historia
    private string[] historia = new string[]
    {
        "Te despiertas tras otra pesadilla.",
        "Desde hace tiempo, sueñas con lo mismo...\nLa ausencia de tu peluche.",
        "Dicen que está en el ático.\nUn lugar prohibido.",
        "Nadie puede subir allí.",
        "Pero necesitas recuperarlo.",
        "Esta noche... decidirás romper las reglas."
    };
    // Índice que indica en qué frase estamos actualmente
    private int indice = 0;

    // Contador interno para medir el tiempo
    private float contadorTiempo = 0f;
    private InputAction _interact;
    private float _timer;
    private float _delayTime = 0.00001f;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Start()
    {
        _interact = InputSystem.actions.FindAction("Interact");
        if (_interact == null)
        {
            Debug.Log("No hay interact");
        }
        // Muestra la primera frase de la historia
        textoUI.text = historia[indice];
    }

    void Update()
    {
        //01. CONTROLA SI LA ESCENA HA TERMINADO

        // Si el índice actual ya está en la última frase del array...
        // (Length - 1 porque los arrays empiezan en 0)
        // Si ya terminó la historia, no hacemos nada
        if (indice >= historia.Length - 1)
            // Salimos del método y no seguimos ejecutando nada más
            return;

        //02. CONTROL DEL TIEMPO

        // Acumula el tiempo transcurrido entre frames
        // Time.deltaTime representa el tiempo entre frames
        contadorTiempo += Time.deltaTime;
        // Si el contador ha alcanzado el tiempo que queremos esperar
        // (se puede configurar en el inspector¡)
        if (contadorTiempo >= tiempoEntreFrases || _interact.WasPressedThisFrame() )
        {
            if (_timer >= _delayTime)
            {
                // Reiniciamos el contador a 0 para empezar a contar otra vez
                contadorTiempo = 0f;
                // Avanzamos al siguiente índice del array (siguiente frase)
                indice++;
                // Actualizamos el texto en pantalla con la nueva frase
                textoUI.text = historia[indice];
                _timer = 0f;
            }

        //03. ACTIVAR BOTÓN AL FINAL

            // Si después de avanzar hemos llegado a la última frase...
            if (indice == historia.Length - 1)
            {
                // Activamos el botón para ir a la escena definida
                GameManager.Instance.ChangeToNextScene(3);
            }
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion

} // class IntroControler 
// namespace

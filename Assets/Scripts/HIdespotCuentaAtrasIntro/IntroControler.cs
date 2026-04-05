//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo  JESUS DIEZ
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System;
using UnityEngine;
//Directiva control de de escenas
using UnityEngine.SceneManagement;
// Directiva de la IU
using UnityEngine.UI;
// Directiva textos TMP
using TMPro; 

/// <summary>
//Contrala la escena que narra
//la historia del niño y sus objetos del juego

/// </summary>
public class IntroControler : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    
    // Referencia al texto de la UI donde se mostrará la historia
    [SerializeField] private TextMeshProUGUI textoUI;

    // Tiempo (en segundos) que se muestra cada frase
    [SerializeField] private float tiempoEntreFrases = 3f;

    // Referencia al botón para continuar (ir a la siguiente escena)
    [SerializeField] private GameObject botonContinuar;

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

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Start()
    {
        //Desactivama el panel con la historial al comenzar
        botonContinuar.SetActive(false);
        //Asigna al texto del panel cada elemento(frase) del array co los textos
        textoUI.text = historia[indice];
    }

    void Update()
    {
        //01. CONTROL DE FINAL DE HISTORIA

        // Si el índice actual ya está en la última frase del array...
        // (Length - 1 porque los arrays empiezan en 0)
        // Si ya terminó la historia, no hacemos nada
        if (indice >= historia.Length - 1)
            // Salimos del método y no seguimos ejecutando nada más
            return;
        
        //02. CONTROL DEL TIEMPO

        // Sumamos al contador el tiempo que ha pasado desde el último frame
        // Time.deltaTime = tiempo real entre frames
        contadorTiempo += Time.deltaTime;
        // Si el contador ha alcanzado el tiempo que queremos esperar
        // (se puede configurar en el inspector¡)
        if (contadorTiempo >= tiempoEntreFrases)
        {
            // Reiniciamos el contador a 0 para empezar a contar otra vez
            contadorTiempo = 0f;
            // Avanzamos al siguiente índice del array (siguiente frase)
            indice++;
            // Actualizamos el texto en pantalla con la nueva frase
            textoUI.text = historia[indice];

        //03. ACTIVAR BOTÓN AL FINAL

            // Si después de avanzar hemos llegado a la última frase...
            if (indice == historia.Length - 1)
            {
            // Activamos el botón para ir a la escena definida
                botonContinuar.SetActive(true);
            }
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    //Lleva a escana1, se asigna al boton
    //Esta comentado, ya que he puesto el prefab del boton tiene ya un script de cambio de escana
    //public void IrAEscena1()
    //{
    //  SceneManager.LoadScene("Escena1");
    //}

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion

} // class IntroControler 
// namespace

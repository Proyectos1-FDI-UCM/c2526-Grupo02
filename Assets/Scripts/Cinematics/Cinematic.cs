//---------------------------------------------------------
// Script que nos permite mover una imagen en un canvas dandole unas coordenadas para "animar" los comics
// Alejandro Jiménez Rojo
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;



public class Cinematic : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    [SerializeField]
    Vector3[] Coords;//Coordenadas a las que moveremos el comic para hacer una ligera animación entre escena y escena
    [SerializeField]
    float Speed;    //velocidad a la que se hará el cambio de panel de comic
    [SerializeField]
    UnityEngine.UI.Image comic; //la imagen que tendra al comic
    [SerializeField]
    int NextRoom; //Valor de la escena a la que debemos ir tras terminar el comic;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    [SerializeField]
    private bool _active = false;//atributo que controla si está activado o no el canvas
    private int i = 0; //Puntero que mantiene la posición en la que estamos del comic
    private InputAction _Interact; //Acción interact
    private float _timer = 0;//Contador para frenar las teclas
    

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
   void Start()
    {
        _Interact = InputSystem.actions.FindAction("Interact"); //asignamos la accion de interact
        //Programación defensiva
        if (_Interact == null)
        {
            Debug.Log("No se encontró la acción Interact");
            return;
        }
        if (comic == null)
        {
            Debug.Log("Falta configurar el comic");
            return;
        }
        comic.enabled = false;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //Si está activa la cinemática
        if (_active)
        {
            
            //Se activa la imagen
            comic.enabled = true;
            //si se presiona la tecla de interactuar y no ha sido demasiado rápido se cambia de panel del comic (coordenadas)
            if (_Interact.WasPressedThisFrame() && _timer <= 0 && !Pausa_controller.IsGamePaused)
            {
                _timer = 1;
                i++;
            }
            //Si terminamos el array de coordenadas nos teletransporta a la siguiente habitación
            if (i >= Coords.Length)
            {
                GameManager.Instance.ChangeScene(NextRoom);
            }
            //Manejo del temporizador que evita que presionemos demasiado rápido
            if (_timer >= 0)
            {
                _timer -= Time.deltaTime;
            }
            //Ajustamos las coordenadas del comic con un lerp mientras no sea la objetivo
            if (i < Coords.GetLength(0) && comic.rectTransform.localPosition != Coords[i])
            {
                comic.rectTransform.localPosition = Vector3.Lerp(comic.rectTransform.localPosition, Coords[i], Speed);
            }
        }
        else
        {
            //Se desactiva el comic
            comic.enabled = false;
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos público
    //método que llamaremos para activar la cinemática
    public void  CinematicSetActive()
    {
        _active = true;
    }

    #endregion
   

} // class Cinematic 
// namespace

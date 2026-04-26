//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
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
        if (_Interact != null)
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
        if (_active)
        {
            comic.enabled = true;
            if (_Interact.WasPressedThisFrame() && _timer <= 0)
            {
                _timer = 1;
                i++;
            }
            if (i >= Coords.Length)
            {
                GameManager.Instance.ChangeScene(NextRoom);
            }
            if (_timer >= 0)
            {
                _timer -= Time.deltaTime;
            }
            if (comic.rectTransform.localPosition != Coords[i])
            {
                comic.rectTransform.localPosition = Vector3.Lerp(comic.rectTransform.localPosition, Coords[i], Speed);
            }
        }
        else
        {
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

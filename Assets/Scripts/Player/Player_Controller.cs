//---------------------------------------------------------
// Script que maneja el movimiento del jugador
// Responsable de la creación de este archivo: Alejandro Jiménez Rojo
// Dont go up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    //Atributo que nos dice la velocidad máxima del jugador.
    [SerializeField]
    int Speed = 5;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
  
    //Atributo de la acción para moverse
    private InputAction _move;
    //Atributo que coje el rigidBody del jugador.
    private Rigidbody2D _rb;
    //atributo donde guardamos la velocidad que hemos metido en el inspector,
    private int SpeedRecord;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    void Start()
    {
        SpeedRecord = Speed;
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            Debug.Log("No hay Rigidbody");
            return;
        }
        _move = InputSystem.actions.FindAction("Move");
        if (_move == null)
        {
            Debug.Log("No se ha encontrado la acción move");
            return;
        }
    }
    void Update()
    {
        //Calculamos la dirección del movimiento y se la sumamos a la posición x multiplicandolo por la velocidad y el time.deltatime
        Vector2 dir = _move.ReadValue<Vector2>();
        float HorizontalDir = Mathf.Round(dir.x);
        Quaternion rot = transform.rotation;
        Vector2 pos = transform.position;

        //Redondeamos el valor de dir.x para que en todas las plataformas y controladores el movimiento sea igual.
        pos.x += HorizontalDir * Speed * Time.deltaTime;
        transform.position = pos;

        //Calculamos la dirección a la que mira el jugador
        rot.x = 0;
        rot.z = 0;
        if(Speed != 0)
        {
            if (HorizontalDir == -1)
            {
                rot.y = 180;
            }
            else if (HorizontalDir == 1)
            {
                rot.y = 0;
            }
            transform.rotation = rot;
        }
        
    }
    #endregion


    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public void Stop()
    {
        //Ponemos la velocidad a 0 para que no se mueva el jugador (RECORDAR QUE NO SE LLAME CONSTANTEMENTE)
        Speed = 0;
    }
    public void Resume()
    {
        //Ponemos la velocidad a la que tenia anterior mente para que se mueva el jugador (RECORDAR QUE NO SE LLAME CONSTANTEMENTE)
        Speed = SpeedRecord;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    #endregion
} 

//---------------------------------------------------------
// Script que maneja los estados de los enemigos.
// Responsable de la creación de este archivo : Alejandro Jiménez Rojo
// Dont go up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;


public class Enemy_Detect : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    //Atributo que nos dice cuanto tiempo hay entre el estado 0 y el último.
    [Header("Tiempo entre estado 0 y N en segundos")]
    [SerializeField]
    private float MaxStateTime;
    //Atributo que nos dice cuantos estados tiene este enemigo (Sirve para hacerlo más escalable)
    [Header("Número de estados")]
    [SerializeField]
    private int NumStates;
    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    //Variable que maneja el estado en el que estamos (Inicializado a 0)
    private int _state = 0;
    //Contador del tiempo que llevamos dentro del campo de visión
    private float _time = 0;
    //Atributo que nos dice cuanto tiempo hay entre estados.
    private float _stateInBetweenTime = 0;
    //Atributo que nos señala si el jugador esta dentro o no
    private bool _playerIsInside;
    //Variable para esconderse
    private HideSystem HideSystem;
    
  
    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Revisamos si el objeto que ha entrado es un jugador (tiene el player_Controller)
        if (collision.GetComponent<Player_Controller>() != null)
        {
            //El jugador está dentro
            _playerIsInside = true;
            if (collision.GetComponent<HideSystem>() == null)
            {
                Debug.Log("No has metido el HIDE SYSTEM");
            }
            else
            {
                HideSystem = collision.GetComponent<HideSystem>();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Revisamos si el objeto que ha salido es un jugador (tiene el player_Controller)
        if (collision.GetComponent<Player_Controller>() != null)
        {
            //El jugador ya no está dentro
            _playerIsInside = false;
            HideSystem = null;
           
        }
    }


    private void Start()
    {
        //Calculamos el tiempo entre estados según el número de estados que hay y el tiempo entre el estado 0 y el N
        //Con esto TODOS LOS ESTADOS DURAN LO MISMO
        _stateInBetweenTime = MaxStateTime / NumStates;

    }
    void Update()
    {
        // si el jugador está dentro vamos contando el tiempo hacia arriba
        if (_playerIsInside && !HideSystem.IsHiding && !Pausa_controller.IsGamePaused)
        {
            //Si el tiempo es menor que el máximo, se sigue contando hacia arriba
            if (_time <= MaxStateTime)
            {
                _time += Time.deltaTime;
            }

            //Si el tiempo que llevamos es mayor que el tiempo
            //entre estados por el estado actual se suma uno al estado
            //Además solo lo hace mientras el tiempo no supere al máximo (para evitar que 
            //Con esto conseguimos que podamos tener muchos estados y que se manejen todos
            if (_time > _stateInBetweenTime*_state && _time < MaxStateTime)
            {
                _state++;
                Debug.Log(_state);
            }
        }
        else
        {
            //Manejamos el contador en reversa 
            if (_time > 0 && !Pausa_controller.IsGamePaused)
            {
                _time -= Time.deltaTime;

                if (_time < _stateInBetweenTime * _state)
                {
                    if(_state > 0)
                    {
                        _state--;
                    }
                    Debug.Log(_state);
                }
            }
            else if( _time <  0 )
            {
                _time = 0;
            }
            
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    public int GetState()
        { return _state; }
    #endregion
    
    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)

    #endregion   

} // class Enemy_Detect 


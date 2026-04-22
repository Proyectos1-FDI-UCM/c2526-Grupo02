//---------------------------------------------------------
// Este script nos permite hacer visible o invisible un objeto al collisionar con el, se usa principalmente en el tutorial para mostrar los controles
// Alejandro Jiménez Rojo
// Don't Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class PopUp : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

   
    [SerializeField]
    float PopUpSpeed; //Atributo de la velocidad a la que queremos que aparezca

    #endregion
    
    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    

    private Collider2D _collider; // el collider del objeto a usar
    private bool _visible = false; //booleana con la que controlaremos el popup
    private Color _alpha; //color con el que controlaremos el alpha
    private GameObject _obj;//Este objeto que usaremos.
    private SpriteRenderer _spr;//sprite de este objeto

    #endregion
    
    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour
    
    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 
    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        _alpha = new Color(1,1,0,0);
        _obj = this.gameObject;
        _spr = _obj.GetComponent<SpriteRenderer>();
        _collider = _obj.GetComponent<Collider2D>();
        if(_spr == null)
        {
            Debug.Log("No hay sprite renderer");
            return;
        }
        if (_collider == null || !_collider.isTrigger )
        {
            Debug.Log("No hay collider configurado en el PopUp / no esta puesto en modo trigger");
            return;
        }
        _spr.color = _alpha;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _visible = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _visible = false;
    }

     void Update()
    {
        if (_visible)
        {
            //Si el alpha no es 1 (255) hacemos un lerp y vamos ajustando el alpha para hacerlo visibles
            if (_alpha.a < 1)
            {
                _alpha.a += PopUpSpeed;
                 _spr.color = _alpha;
            }
        }
        else
        {
            //Si el alpha no es 0 hacemos un lerp y vamos ajustando el alpha para hacerlo transparente
            if (_alpha.a > 0)
            {
                _alpha.a -= PopUpSpeed;
                _spr.color = _alpha;
            }
        }
    }
    #endregion

   

} // class PopUp 
// namespace

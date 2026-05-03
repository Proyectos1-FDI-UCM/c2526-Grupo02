//---------------------------------------------------------
// Gestor de escena. Podemos crear uno diferente con un
// nombre significativo para cada escena, si es necesario
// Guillermo Jiménez Díaz, Pedro Pablo Gómez Martín
// Template-P1
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Componente que se encarga de la gestión de un nivel concreto.
/// Este componente es un singleton, para que sea accesible para todos
/// los objetos de la escena, pero no tiene el comportamiento de
/// DontDestroyOnLoad, ya que solo vive en una escena.
///
/// Contiene toda la información propia de la escena y puede comunicarse
/// con el GameManager para transferir información importante para
/// la gestión global del juego (información que ha de pasar entre
/// escenas)
/// </summary>
public class LevelManager : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----

    #region Atributos del Inspector (serialized fields)

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Inv;
    [SerializeField]
    private GameObject DeathScreen;
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private Flags FlagManager;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----

    #region Atributos Privados (private fields)

    /// <summary>
    /// Instancia única de la clase (singleton).
    /// </summary>
    private static LevelManager _instance;
    private Vector3 _playerTransform;
    private bool[] _puzzleState;
    private Object[] _invState;
    private const int _offsetCam = -10;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----

    #region Métodos de MonoBehaviour

    protected void Awake()
    {
        if (_instance == null)
        {
            // Somos la primera y única instancia
            _instance = this;
            Init();
        }
    }

    #endregion

    // ---- MÉTODOS PÚBLICOS ----

    #region Métodos públicos
    public Flags GetFlags()
    {
        return FlagManager;
    }
    public void MuerteJugador()
    {
        DeathScreen.SetActive(true);
    }
    public GameObject GetPlayer() { return Player; }

    public GameObject GetInv() { return Inv; }

    public void GameOver() //activa la pantalla de game over y desactiva al jugador
    {
        DeathScreen.SetActive(true);
        Player.SetActive(false);
    }
    public void Respawn() //activa al jugador, lo mueve a el y a la camara al ultimo checkpoint con el ultimo estado guardado
    {
        Player.SetActive(true);
        Player.transform.position = _playerTransform;
        Vector3 Cam = _playerTransform;
        Cam.z = _offsetCam;
        Camera.GetComponent<Follow_Player>().Teleport(Cam);
        DeathScreen.SetActive(false);
        Inv.GetComponent<Inventory_Manager>().LoadState(_invState);
        for (int i = 0; i < FlagManager.GetFlagLenght(); i++)
        {
            FlagManager.CambiaFlag((Flags.Conditions)i, _puzzleState[i]);
        }

    }

    public void SaveInv(int i) //carga el estado del inventario
    {
        _invState[i] = Inv.GetComponent<Inventory_Manager>().RetState(i);
    }
    public void SavePuz(int i) // carga el estado de las flags
    {

        bool flag = FlagManager.GetComponent<Flags>().GetPos((Flags.Conditions)i);
        _puzzleState[i] = flag;

    }
    public void SavePos(Vector3 pos) // carga la pos del jugador
    {
        _playerTransform = pos;
    }


    /// <summary>
    /// Propiedad para acceder a la única instancia de la clase.
    /// </summary>
    public static LevelManager Instance
    {
        get
        {
            Debug.Assert(_instance != null);
            return _instance;
        }
    }

    /// <summary>
    /// Devuelve cierto si la instancia del singleton está creada y
    /// falso en otro caso.
    /// Lo normal es que esté creada, pero puede ser útil durante el
    /// cierre para evitar usar el LevelManager que podría haber sido
    /// destruído antes de tiempo.
    /// </summary>
    /// <returns>Cierto si hay instancia creada.</returns>
    public static bool HasInstance()
    {
        return _instance != null;
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----

    #region Métodos Privados

    /// <summary>
    /// Dispara la inicialización.
    /// </summary>
    private void Init()
    {
        _puzzleState = new bool[FlagManager.GetFlagLenght()];
        _invState = new Object[Inv.GetComponent<Inventory_Manager>().IniState()];
    }

    #endregion
} // class LevelManager 
// namespace
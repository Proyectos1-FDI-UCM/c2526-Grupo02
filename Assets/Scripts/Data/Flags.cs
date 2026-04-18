//---------------------------------------------------------
// Lleva una cuenta del progreso
// Responsable de la creación de este archivo - Pablo
// Don´t Go Up
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using System.Diagnostics;
using UnityEngine;


/// <summary>
/// Es un sitema que comprueba cuando se ha cumplido cierta condición y lo guarda
/// Su utilida recide en que es un guardado de progreso para el juego 
/// Es un array de bools, y luego un enum publico para darle nombres
/// </summary>
public class Flags : MonoBehaviour
{

    public enum Conditions {CocineroDistraido, puzzle1, HablarConChiquilla, ContadorApagado, puzzle2, puzzle3,Sotano}


    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)

    private bool[] _flags;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Awake()
    {
        //Crea un array de bools del tamaño del enumerado Conditions
        _flags = new bool[Conditions.GetNames(typeof(Conditions)).Length];
        UnityEngine.Debug.Log(_flags.Length + " BANDERAS");
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    public bool GetPos(Conditions n) //Comprueba la posicion de n en Conditions para buscarla en flags
    {
        int i = (int) n;
        return _flags[i];
    }
    public int GetFlagLenght() //Busca el tamaño de Flags
    {
        return Conditions.GetNames(typeof(Conditions)).Length;
    }

    //Versión compatible con UNITY EVENTS de CAMBIAFLAG
    public void CambiaFlagINSPECTOR(int n) //Cambia a true flag
    {
        int i = (int)n;
        _flags[i] = true;
    }

    public void CambiaFlag(Conditions n, bool Cambio) //Cambia el valor de flag
    {
        int i = (int)n;
        _flags[i] = Cambio;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion

} // class Flags 
// namespace

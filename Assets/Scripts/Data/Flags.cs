//---------------------------------------------------------
// Lleva una cuenta del progreso
// Responsable de la creación de este archivo - Pablo
// Nombre del juego
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

    private bool[] flags;

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    void Awake()
    {
        flags = new bool[Conditions.GetNames(typeof(Conditions)).Length];
        UnityEngine.Debug.Log(flags.Length + " BANDERAS");
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos

    public bool GetPos(Conditions n)
    {
        int i = (int) n;
        return flags[i];
    }
    public int GetFlagLenght()
    {
        return Conditions.GetNames(typeof(Conditions)).Length;
    }

    //Versión compatible con UNITY EVENTS de CAMBIAFLAG
    public void CambiaFlagINSPECTOR(int n)
    {
        int i = (int)n;
        flags[i] = true;
    }

    public void CambiaFlag(Conditions n, bool Cambio) 
    {
        int i = (int)n;
        flags[i] = Cambio;
    }
    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados

    #endregion

} // class Flags 
// namespace

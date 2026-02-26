//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Nombre del juego
// Proyectos 1 - Curso 2025-26
//---------------------------------------------------------

using UnityEngine;
// Añadir aquí el resto de directivas using

[System.Serializable]
public struct Object
{
    [SerializeField]
    private int index;
    [SerializeField]
    private objType ThisObj;
    [SerializeField]
    private string Description;


    public void SetIndex(int _inputIndex)
    {
        index = _inputIndex;
    }
    public void SetObj(objType obj)
    {
        ThisObj = obj;
    }
    public void SetDesc(string desc)
    {
        Description = desc;
    }
    public objType Getobj()
    {
        return ThisObj;
    }
}
public enum objType { vacio,test1,test2,test3,test4 }



// namespace

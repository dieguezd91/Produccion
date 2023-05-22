using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Character/Create new move")]

public class MoveBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] CharacterType characterType;

    //Stats
    [SerializeField] int fuerza;
    [SerializeField] int destreza;
    [SerializeField] int defensa;
    [SerializeField] int presicion;
    [SerializeField] int evasion;
    [SerializeField] int critico;
    [SerializeField] int pp;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public CharacterType CharType
    {
        get { return characterType; }
    }

    public int Fuerza
    { 
        get { return fuerza; }     
    }

    public int Destreza
    { 
        get { return destreza; }     
    }

    public int Defensa
    { 
        get { return defensa; }     
    }

    public int Presicion
    { 
        get { return presicion; }     
    }

    public int Evasion
    { 
        get { return evasion; }     
    }

    public int Critico
    { 
        get { return critico; }     
    }

    public int Pp
    { 
        get { return pp; }     
    }
}

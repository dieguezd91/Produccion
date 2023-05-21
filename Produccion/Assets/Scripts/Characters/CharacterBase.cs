using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create new character")]

public class CharacterBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;
    [SerializeField] Sprite charSprite;
    [SerializeField] CharacterType characterType;

    //Stats
    [SerializeField] int maxHp;
    [SerializeField] int strenght;
    [SerializeField] int dexterity;
    [SerializeField] int defense;
    [SerializeField] int speed;

    [SerializeField] int expYield;
    [SerializeField] GrowthRate growthRate;

    [SerializeField] List<AvailableAttack> availableAttacks;

    public GrowthRate GrowthRate => GrowthRate;

    //public int GetExpForLevel(int level)
    //{

    //}

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite CharSprite
    {
        get { return charSprite; }
    }

    public CharacterType CharType
    {
        get { return characterType; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Strenght
    {
        get { return strenght; }
    }

    public int Dexterity
    {
        get { return dexterity; }
    }

    public int Defense
    {
        get { return defense; }
    }
    
    public int Speed
    {
        get { return speed; }
    }

    public int ExpYield
    {
        get { return expYield; }
    }

    public List<AvailableAttack> Attacks
    {
        get { return availableAttacks; }
    }
}

[System.Serializable]

public class AvailableAttack
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    { 
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}

public enum CharacterType
{
    Player,
    Patrulla,
    Maton,
    MiniBoss,
    Boss,
    Dron
}

public enum GrowthRate
{
    MediumFast, 
    Fast
}

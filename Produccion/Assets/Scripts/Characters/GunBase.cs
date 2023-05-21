using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Gun/Create new gun")]

public class GunBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;
    [SerializeField] Sprite gunSprite;
    [SerializeField] GunType gunType;

    [SerializeField] int attackMelee;
    [SerializeField] int attackRange;
    [SerializeField] int attackThrowable;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite GunSprite
    {
        get { return gunSprite; }
    }

    public GunType GunType
    {
        get { return gunType; }
    }

    public int AttackMelee
    {
        get { return attackMelee; }
    }

    public int AttackRange
    {
        get { return attackRange; }
    }

    public int AttackThrowable
    {
        get { return attackThrowable; }
    }
}

public enum GunType
{
    Melee,
    Range,
    Throwable
}
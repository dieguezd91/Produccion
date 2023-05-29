using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] Sprite icon;
    [SerializeField] int price;

    public string Name => name;
    public int Price => price;

    public string Description => description;

    public Sprite Icon => icon;

    public virtual bool Use(Character character)
    {
        return false;
    }
}

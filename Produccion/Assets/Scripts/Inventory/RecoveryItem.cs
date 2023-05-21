using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create new recvory item")]

public class RecoveryItem : ItemBase
{
    [Header("HP")]
    [SerializeField] int hpAmount;
    [SerializeField] bool restoreMaxHP;

    public override bool Use(Character character)
    {
        if (hpAmount > 0)
        {
            if (character.HP == character.MaxHp)
                return false;

            character.DecreaseHP(hpAmount);
        }

        return true;
    }


}

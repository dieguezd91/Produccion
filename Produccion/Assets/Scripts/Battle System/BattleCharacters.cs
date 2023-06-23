using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacters : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] string[] attacksAvailable;

    public string characterName;
    public int currentHP, maxHP, dexterity, strength, defence, meleeWeaponDamage, rangeWeaponDamage;
    public bool isDead;

    public bool IsPlayer()
    {
        return isPlayer;
    }

    public string[] AttackMovesAvailable()
    {
        return attacksAvailable;
    }

    /*public void TakeHPMeleeDamage(int meleeDamageToReceive)
    {
        currentHP -= meleeDamageToReceive;

        if(currentHP < 0)
        {
            currentHP = 0;
        }
    }*/
    
    public void TakeHPDamage(int damageToReceive)
    {
        currentHP -= damageToReceive;

        if(currentHP < 0)
        {
            currentHP = 0;
        }
    }

    public void UseItemInBattle(ItemsManager itemToUse)
    {
        if(itemToUse.itemType == ItemsManager.ItemType.Item)
        {
            if(itemToUse.affectType == ItemsManager.AffectType.HP)
            {
                if (PlayerStats.instance.currentHP < PlayerStats.instance.maxHP)
                    AddHP(itemToUse.amountOfAffect);
            }            
        }
    }

    private void AddHP(int amountOfAffect)
    {
        currentHP += amountOfAffect;
    }
}

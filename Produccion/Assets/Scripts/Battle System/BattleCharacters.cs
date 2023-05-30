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
}

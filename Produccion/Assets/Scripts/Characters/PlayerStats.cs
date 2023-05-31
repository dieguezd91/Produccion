using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [SerializeField] public string playerName;
    [SerializeField] public Sprite characterImage;

    [SerializeField] public int playerLevel = 1;
    [SerializeField] public int maxLevel = 50;
    [SerializeField] public int currentXP;
    [SerializeField] public int[] xpForNextLevel;
    [SerializeField] public int baseLevelXP = 100;

    [SerializeField] public int maxHP = 100;
    [SerializeField] public int currentHP;

    [SerializeField] public int dexterity;
    [SerializeField] public int strength;
    [SerializeField] public int defence;

    public string equippedMeleeWeaponName;
    public string equippedRangeWeaponName;

    public int meleeDamage;
    public int rangeDamage;

    public ItemsManager equipedMeleeWeapon, equipedRangeWeapon;

    private void Start()
    {
        instance = this;

        xpForNextLevel = new int[maxLevel];
        xpForNextLevel[1] = baseLevelXP;

        for(int i = 2; i < xpForNextLevel.Length; i++)
        {
            xpForNextLevel[i] = (int)(0.02f * i * i * i + 3.06f * i * i + 105.6f * i);

        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            AddXP(20);
        }
    }

    public void AddXP(int amountOfXp)
    {
        Debug.Log(currentXP);
        Debug.Log(amountOfXp);
        currentXP += amountOfXp;
        if(currentXP > xpForNextLevel[playerLevel])
        {
            currentXP -= xpForNextLevel[playerLevel];
            playerLevel++;
        }

        if(playerLevel % 2 == 0)
        {
            dexterity++;
            strength++;
        }
        else
        {
            defence++;
        }
        Debug.Log(currentXP);
    }

    public void AddHP(int amountHPToAdd)
    {
        currentHP += amountHPToAdd;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void EquipMeleeWeapon(ItemsManager meleeWeaponToEquip)
    {
        equipedMeleeWeapon = meleeWeaponToEquip;
        equippedMeleeWeaponName = equipedMeleeWeapon.itemName;
        meleeDamage = equipedMeleeWeapon.weaponStrength;

    }
    
    public void EquipRangeWeapon(ItemsManager rangeWeaponToEquip)
    {
        equipedRangeWeapon = rangeWeaponToEquip;
        equippedRangeWeaponName = equipedRangeWeapon.itemName;
        rangeDamage = equipedRangeWeapon.weaponDexterity;
    }
        
}

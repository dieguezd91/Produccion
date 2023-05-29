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
            AddXP(100);
        }
    }

    public void AddXP(int amountOfXp)
    {
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
    }

    public void AddHP(int amountHPToAdd)
    {
        currentHP += amountHPToAdd;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
}

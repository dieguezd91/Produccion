using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public string playerName;
    [SerializeField] public Sprite charImage;

    [SerializeField] public int playerLevel = 1;
    [SerializeField] int maxLevel = 50;
    [SerializeField] public int currentXP;
    [SerializeField] public int[] xpForNextLevel;
    [SerializeField] int baseLevelXP = 100;


    [SerializeField] public int maxHP = 100;
    [SerializeField] public int currentHP;

    [SerializeField] int dexterity;
    [SerializeField] int strenght;
    [SerializeField] int defence;

    // Start is called before the first frame update
    void Start()
    {
        xpForNextLevel = new int[maxLevel];
        xpForNextLevel[1] = baseLevelXP;

        for(int i = 2; i < xpForNextLevel.Length; i++)
        {
            xpForNextLevel[i] = (int)(0.02f * i * i * i + 3.06f * i * i + 105.6f * i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            AddXP(100);
        }
    }

    public void AddXP(int amountOfXp)
    {
        currentXP += amountOfXp;
        if (currentXP > xpForNextLevel[playerLevel])
        {
            currentXP -= xpForNextLevel[playerLevel];
            playerLevel++;

            if(playerLevel % 2 == 0)
            {
                dexterity++;
                strenght++;
            }
            else
            {
                defence++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;

    public static MenuManager instance;

    private PlayerStats[] playerStats;
    [SerializeField] Text[] nameText, hpText, levelText, xpText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] charImage;
    [SerializeField] GameObject[] charPanel;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        if(menu.activeInHierarchy)
        {
            menu.SetActive(false);
        }
        else 
        {
            UpdateStats();
            menu.SetActive(true);
        }
    }

    public void UpdateStats()
    {
        playerStats = GameManager.instance.GetPlayerStats();

        for (int i = 0; i < playerStats.Length; i++)
        {
            nameText[i].text = playerStats[i].playerName;
            hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
            levelText[i].text = "Level: " + playerStats[i].playerLevel;
            xpText[i].text = "Current XP: " + playerStats[i].currentXP;
            xpSlider[i].maxValue = playerStats[i].xpForNextLevel[playerStats[i].playerLevel];
            xpSlider[i].minValue = playerStats[i].currentXP;
            charImage[i].sprite = playerStats[i].charImage;
        }
        //for(int i = 0; i < playerStats.Length; i++)
        //{
        //    print(i);
        //    charPanel[i].SetActive(true);
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStats : MonoBehaviour
{
    public GameObject menuStats;
    bool menuStatsIsActive;

    public void Awake()
    {
        menuStats.SetActive(false);
        menuStatsIsActive = false;
    }

    private void Update()
    {
        Stats();
    }

    public void Stats()
    {
        if (Input.GetKeyDown(KeyCode.C) && !menuStatsIsActive)
        {
            menuStats.SetActive(true);
            menuStatsIsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && menuStatsIsActive)
        {
            menuStats.SetActive(false);
            menuStatsIsActive = false;
        }
    }
}

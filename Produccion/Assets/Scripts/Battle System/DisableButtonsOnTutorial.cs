using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButtonsOnTutorial : MonoBehaviour
{
    public GameObject runButton;
    public GameObject itemsButton;
    public GameObject rangeButton;

    void Update()
    {
        if (BattleManager.instance.bossBattle || BattleManager.instance.dinniesBattle)
        {
            runButton.SetActive(false);
            itemsButton.SetActive(true);
            rangeButton.SetActive(true);
        }
        else if(GameManager.instance.tutorial)
        {
            runButton.SetActive(false);
            itemsButton.SetActive(false);
            rangeButton.SetActive(false);
        }
        else
        {
            runButton.SetActive(true);
            itemsButton.SetActive(true);
            rangeButton.SetActive(true);
        }
    }
}

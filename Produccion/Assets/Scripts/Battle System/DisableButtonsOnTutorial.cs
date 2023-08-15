using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButtonsOnTutorial : MonoBehaviour
{
    public Button runButton;
    public Button rangeButton;

    void Update()
    {
        if (BattleManager.instance.bossBattle || BattleManager.instance.dinniesBattle)  
            runButton.interactable = false;
        else if(GameManager.instance.tutorial)
            runButton.interactable = false; 
        if (PlayerStats.instance.equipedRangeWeapon != null && Inventory.instance.hasAmmo == true)
            rangeButton.interactable = true;
        else
            rangeButton.interactable = false;        
    }
}

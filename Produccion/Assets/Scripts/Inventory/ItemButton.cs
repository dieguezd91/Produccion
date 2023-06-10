using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{

    public ItemsManager itemOnButton;
    
    public void Press()
    {        
        MenuManager.instance.itemName.text = itemOnButton.itemName;
        MenuManager.instance.itemDescription.text = itemOnButton.itemDescription;
        MenuManager.instance.activeItem = itemOnButton;
        MenuManager.instance.itemsDescription.SetActive(true);

        if(BattleManager.instance.itemsToUseMenu.activeInHierarchy)
        {
            BattleManager.instance.SelectedItemToUse(itemOnButton);
        }
    }
}

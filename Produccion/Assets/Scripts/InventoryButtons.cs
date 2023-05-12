using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{
    Inventory inventory;
    GameManagerSingleton gameManager;

    private void Start()
    {
        gameManager = GameManagerSingleton.instance;
        inventory = gameManager.GetComponent<Inventory>();
    }

    public void UseItem()
    {
        inventory.UseInventoryItems(gameObject.name);
    }
}

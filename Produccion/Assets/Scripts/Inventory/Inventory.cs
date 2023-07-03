using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<ItemsManager> itemsList;
    public int credits;
    public bool hasCompletedDinniedTutorial;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        itemsList = new List<ItemsManager>();
    }

    public void AddItems(ItemsManager item)
    {
        if (item.isStackable)
        {
            bool itemAlreadyInInventory = false;

            foreach (ItemsManager itemInInventory in itemsList)
            {
                if (itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount++;
                    itemAlreadyInInventory = true;
                }
            }

            if (!itemAlreadyInInventory)
            {
                itemsList.Add(item);
            }
        }
        else
        {
            itemsList.Add(item);
        }
    }

    public void RemoveItem(ItemsManager item)
    {
        if (item.isStackable)
        {
            ItemsManager inventoryItem = null;
            foreach (ItemsManager itemInInventory in itemsList)
            {
                if (itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount--;
                    inventoryItem = itemInInventory;
                }
            }

            if (inventoryItem != null && inventoryItem.amount <= 0)
            {
                itemsList.Remove(inventoryItem);
            }
        }
        else
        {
            itemsList.Remove(item);
        }
    }

    public void AddCredits(int creditsToGive)
    {
        credits += creditsToGive;
        Debug.Log("Ganaste " + creditsToGive.ToString() + " creditos");
    }

    public List<ItemsManager> GetItemsList()
    {
        return itemsList;
    }
}

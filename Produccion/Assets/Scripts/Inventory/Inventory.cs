using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<ItemsManager> itemsList;
    public int credits;
    public bool hasRookiePistol;
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
        if(item.isStackable)
        {
            bool itemAlreadyInInventory = false;

            foreach (ItemsManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }

            if(!itemAlreadyInInventory)
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
        if(item.isStackable)
        {
            ItemsManager inventoryItem = null;
            foreach(ItemsManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount--;
                    inventoryItem = itemInInventory;
                }
            }

            if(inventoryItem != null && inventoryItem.amount <=0)
            {
                itemsList.Remove(inventoryItem);
            }
        }
        else
        {
            itemsList.Remove(item);
        }
    }


    public List<ItemsManager> GetItemsList()
    {
        return itemsList;
    }

    //    [SerializeField] List<ItemSlot> slots;

    //    public List<ItemSlot> Slots => slots;

    //    public List<ItemBase> items;

    //    public int credits;

    //    public ItemBase UseItem(int itemIndex, Character character)
    //    {
    //        var item = slots[itemIndex].Item;
    //        bool itemUsed = item.Use(character);
    //        if(itemUsed)
    //        {
    //            RemoveItem(item);
    //            return item;
    //        }

    //        return null;
    //    }

    //    public void AddItem(ItemBase item)
    //    {   
    //        for(int n = 0; n < slots.Count; n++)
    //        {
    //            if (item.Name == slots[n].Item.Name)
    //            {
    //                slots[n].Count++;
    //            }
    //        }
    //    }

    //    public void RemoveItem(ItemBase item)
    //    {
    //        var itemSlot = slots.First(slot => slot.Item == item);
    //        itemSlot.Count--;
    //        if (itemSlot.Count == 0)
    //            slots.Remove(itemSlot);
    //    }

    //    public static Inventory GetInventory()
    //    {
    //        return FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    //    }
    //}

    //[Serializable]

    //public class ItemSlot
    //{
    //    [SerializeField] ItemBase item;
    //    [SerializeField] int count;

    //    public int Count
    //    {
    //        get => count;
    //        set => count = value;
    //    }
    //    public ItemBase Item
    //    {
    //        get => item;
    //        set => item = value;
    //    }
}

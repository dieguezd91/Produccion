using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryUIState { ItemSelection, CharacterSelection, Busy}

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotUI itemSlotUI;
    [SerializeField] Image itemIcon;
    [SerializeField] Text itemDescription;

    [SerializeField] Character character;

    int selectedItem = 0;
    InventoryUIState state;

    List<ItemSlotUI> slotUIList;

    Inventory inventory;

    private void Awake()
    {
        inventory = Inventory.GetInventory();
    }

    private void Start()
    {
        UpdateItemList();
    }

    void UpdateItemList()
    {
        foreach (Transform child in itemList.transform)
            Destroy(child.gameObject);

        slotUIList = new List<ItemSlotUI>();

        foreach(var itemSlot in inventory.Slots)
        {
            if(itemSlot.Item != null)
            { 
            var slotUIObj = Instantiate(itemSlotUI, itemList.transform);
            slotUIObj.SetData(itemSlot);
            slotUIList.Add(slotUIObj);
            }
        }
        UpdateItemSelection();
    }

    public void HandleUpdate(Action onBack)
    {
        if(state == InventoryUIState.ItemSelection)
        {
            int prevSelection = selectedItem;

            if (Input.GetKeyDown(KeyCode.DownArrow))
                ++selectedItem;
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                --selectedItem;

            selectedItem = Mathf.Clamp(selectedItem, 0, inventory.Slots.Count - 1);

            if (prevSelection != selectedItem)
                UpdateItemSelection();

            if (Input.GetKeyDown(KeyCode.X))
                onBack?.Invoke();


        }
        else if(state== InventoryUIState.CharacterSelection)
        {
            Action onSelected = () =>
            {
                inventory.UseItem(selectedItem, character);
            };

            if (Input.GetKeyDown(KeyCode.Space))
                onSelected?.Invoke();
        }
    }

    void UpdateItemSelection()
    {
        for (int i = 0; i < slotUIList.Count; i++)
        {
            if (i == selectedItem)
                slotUIList[i].NameText.color = GlobalSettings.i.HighlightedColor;
            else
                slotUIList[i].NameText.color = Color.white;
        }

        foreach (var itemSlot in inventory.Slots)
        {
            if (itemSlot.Item != null)
            {
                var item = inventory.Slots[selectedItem].Item;
                itemIcon.sprite = item.Icon;
                itemDescription.text = item.Description;
            }
        }
    }
}

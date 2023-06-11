using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    [SerializeField] List<ItemsManager> itemInfo;
    [SerializeField] GameObject storeItemTemplate;
    Inventory playerInventory;

    void Start()
    {
        playerInventory = GameManager.instance.GetComponent<Inventory>();
        var itemTemplate = storeItemTemplate.GetComponent<TemplateStoreItem>();

        foreach (var item in itemInfo)
        {
            itemTemplate.playerInventory = playerInventory;
            itemTemplate.item = item;
            itemTemplate.iconImage.sprite = item.icon;
            itemTemplate.objectName.text = item.itemName;
            itemTemplate.priceTag.text = item.valueCoins.ToString();

            Instantiate(itemTemplate, transform);
        }
    }
}

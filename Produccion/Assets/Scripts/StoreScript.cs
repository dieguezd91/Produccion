using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    [SerializeField] List<ItemBase> itemInfo;
    [SerializeField] GameObject storeItemTemplate;
    [SerializeField] TextMeshProUGUI totalCoinsText;
    [SerializeField] Inventory playerInventory;
    void Start()
    {
        var itemTemplate = storeItemTemplate.GetComponent<TemplateStoreItem>();

        foreach (var item in  itemInfo) 
        {
            itemTemplate.playerInventory = playerInventory;
            itemTemplate.item = item;
            itemTemplate.icon = item.Icon;
            itemTemplate.objectName.text = item.Name;
            itemTemplate.priceTag.text = item.Price.ToString();

            Instantiate(itemTemplate, transform);
        }
    }

    void Update()
    {
        totalCoinsText.text = playerInventory.credits.ToString();
    }
}

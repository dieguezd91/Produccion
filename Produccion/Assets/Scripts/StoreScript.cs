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
    [SerializeField] TextMeshProUGUI totalCoinsText;
    [SerializeField] Inventory playerInventory;

    void Start()
    {
        var itemTemplate = storeItemTemplate.GetComponent<TemplateStoreItem>();

        foreach (var item in itemInfo)
        {
            itemTemplate.playerInventory = playerInventory;
            itemTemplate.item = item;
            itemTemplate.icon = item.itemsImage;
            itemTemplate.objectName.text = item.itemName;
            itemTemplate.priceTag.text = item.valueCoins.ToString();

            Instantiate(itemTemplate, transform);
        }
    }

    void Update()
    {
        totalCoinsText.text = playerInventory.credits.ToString();
    }
}

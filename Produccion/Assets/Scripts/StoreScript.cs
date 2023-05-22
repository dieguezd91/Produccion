using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    [SerializeField] List<InformationTemplateItem> itemInfo;
    [SerializeField] GameObject storeItemTemplate;
    [SerializeField] TextMeshProUGUI totalCoinsText;
    [SerializeField] InventoryScript playerInventory;
    void Start()
    {
        var itemTemplate = storeItemTemplate.GetComponent<TemplateStoreItem>();

        foreach (var item in  itemInfo) 
        {
            itemTemplate.playerInventory = playerInventory;
            itemTemplate.item = item;
            itemTemplate.image.sprite = item.image;
            itemTemplate.objectName.text = item.objectName;
            itemTemplate.priceTag.text = item.price.ToString();

            Instantiate(itemTemplate, transform);
        }
    }

    void Update()
    {
        totalCoinsText.text = playerInventory.credits.ToString();
    }
}

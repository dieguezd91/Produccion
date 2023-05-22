using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TemplateStoreItem : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI priceTag;
    public TextMeshProUGUI objectName;
    public Button buyButton;
    int price;
    public InformationTemplateItem item;
    public InventoryScript playerInventory;


    void Start()
    {
        price = int.Parse(priceTag.text);
    }

    void Update()
    {
        if(price > playerInventory.credits)
        {
            buyButton.interactable = false;
        }
    }

    public void BuyItem()
    {
        playerInventory.credits -= price;
        playerInventory.items.Add(item);
    }
}

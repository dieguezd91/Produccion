using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TemplateStoreItem : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI priceTag;
    public TextMeshProUGUI objectName;
    public Button buyButton;
    int price;
    public ItemsManager item;
    public Inventory playerInventory;


    void Start()
    {
        playerInventory = Inventory.instance.GetComponent<Inventory>();
        price = int.Parse(priceTag.text);
    }

    void Update()
    {
        if (price > playerInventory.credits)
        {
            buyButton.interactable = false;
        }
    }

    public void BuyItem()
    {
        playerInventory.credits -= price;
        playerInventory.AddItems(item);
    }
}

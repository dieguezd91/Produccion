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
    int totalCoins;

    void Start()
    {
        price = int.Parse(priceTag.text);
    }

    void Update()
    {
        totalCoins = PlayerPrefs.GetInt("totalCoins");
        if(price > totalCoins)
        {
            buyButton.interactable = false;
        }
    }

    public void BuyItem()
    {
        totalCoins -= price;
        PlayerPrefs.SetInt("totalCoins", totalCoins);
    }
}

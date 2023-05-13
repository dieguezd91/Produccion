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
    void Start()
    {
        if (!PlayerPrefs.HasKey("totalCCoinds;"))
        {
            PlayerPrefs.SetInt("totalCoins", 900);
        }

        var itemTemplate = storeItemTemplate.GetComponent<TemplateStoreItem>();

        foreach (var item in  itemInfo) 
        {
            itemTemplate.image.sprite = item.image;
            itemTemplate.objectName.text = item.name;
            itemTemplate.priceTag.text = item.price.ToString();

            Instantiate(itemTemplate, transform);
        }
    }

    void Update()
    {
        totalCoinsText.text = PlayerPrefs.GetInt("totalCoins").ToString();
    }
}

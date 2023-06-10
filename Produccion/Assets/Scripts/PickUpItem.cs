using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] ItemsManager invItem;
    [SerializeField] GameObject door;
    [SerializeField] GameObject dialogue;

    private void Start()
    {
        if(Inventory.instance.hasRookiePistol)
            item.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        item.SetActive(false);
        door.SetActive(true);
        dialogue.SetActive(false);
        Inventory.instance.hasRookiePistol = true;
        Inventory.instance.itemsList.Add(invItem);
    }
}

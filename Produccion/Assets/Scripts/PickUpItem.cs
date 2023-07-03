using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] ItemsManager invItem;
    [SerializeField] GameObject door;
    [SerializeField] GameObject dialogueToDisable;
    Inventory inventory;

    private void Start()
    {
        inventory = GameManager.instance.GetComponent<Inventory>();
        if(inventory.hasRookiePistol)
            item.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        item.SetActive(false);
        door.SetActive(true);
        dialogueToDisable.SetActive(false);
        Inventory.instance.hasRookiePistol = true;
        Inventory.instance.itemsList.Add(invItem);
        GameManager.instance.tutorial = false;
    }
}

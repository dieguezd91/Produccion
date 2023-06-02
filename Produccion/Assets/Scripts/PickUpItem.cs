using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public GameObject item;
    public ItemsManager invItem;
    public GameObject door;
    public Inventory inventory;
    public GameObject dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        item.SetActive(false);
        door.SetActive(true);
        dialogue.SetActive(false);

        inventory.itemsList.Add(invItem);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    [SerializeField] ItemsManager item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Inventory.instance.AddItems(item);
            int index = CollectedItemsManager.instance.GetItemNumber(gameObject);
            Debug.Log(index);
            CollectedItemsManager.instance.collected[index] = true;
        }
    }
}
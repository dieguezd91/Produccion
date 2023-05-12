using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : MonoBehaviour
{
    public float ammoToGive;
    public GameObject itemToAdd;
    public int amountToAdd;
    Inventory inventory;
    GameManagerSingleton gameManager;

    private void Start()
    {
        gameManager = GameManagerSingleton.instance;
        inventory = gameManager.GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory.CheckSlotAvailability(itemToAdd, itemToAdd.name, amountToAdd);

            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] ItemsManager invItem;
    [SerializeField] bool openDoorAfterFight;
    [SerializeField] GameObject door;
    [SerializeField] GameObject objectToDisable;
    [SerializeField] Collider2D eventToEnable;
    Inventory inventory;

    private void Start()
    {
        inventory = GameManager.instance.GetComponent<Inventory>();
        if(inventory.hasCompletedDinniesTutorial)
            item.SetActive(false);
        if (openDoorAfterFight)
            BattleManager.instance.OnBattleEnd += OpenDoor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        item.SetActive(false);
        Inventory.instance.itemsList.Add(invItem);
        GameManager.instance.tutorial = false;
        eventToEnable.enabled = true;
    }

    void OpenDoor(object sender, EventArgs e)
    {
        Debug.Log("aaaa");
        door.SetActive(true);
        objectToDisable.SetActive(false);
        Inventory.instance.hasCompletedDinniesTutorial = true;
    }
}

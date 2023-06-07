using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour
{
    [SerializeField] private GameObject dialogueManager;
    [SerializeField] private DialogueManager dialogueManagerInstance;
    [SerializeField] private BattleManager battleManager;

    public bool fightAfter;
    public string enemies;
    public bool lockDoor;
    public bool onlyOnTutorial;
    public GameObject door;

    private void Start()
    {
        dialogueManagerInstance.OnDialogueEnd += Fight;
        if(onlyOnTutorial && Inventory.instance.hasRookiePistol)
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueManager.SetActive(true);
            if (lockDoor)
                door.SetActive(false);

            GameManager.instance.tutorial = false;
        }
    }

    private void Fight(object sender,EventArgs e)
    {
        if(fightAfter == true)
        {
            battleManager.StartBattle(null, enemies) ;
        }
    }
}

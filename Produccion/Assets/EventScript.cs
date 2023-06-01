using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour
{
    [SerializeField] private GameObject dialogueManager;
    [SerializeField] private DialogueManager dialogueManagerInstance;
    [SerializeField] private BattleManager battleManager;

    private string[] enemies = { "Negro" };

    private void Start()
    {
        dialogueManagerInstance.OnDialogueEnd += Fight;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            dialogueManager.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueManager.SetActive(true);
        }
    }

    private void Fight(object sender,EventArgs e)
    {
        Debug.Log("Fight!");
        battleManager.StartBattle(enemies);
    }
}

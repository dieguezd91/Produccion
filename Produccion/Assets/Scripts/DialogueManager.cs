using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public PlayerController player;

    public bool disableAfter;
    public GameObject collisionEvent;

    public string[] lines;

    public float textSpeed = 0.05f;

    int index;

    float lastSpeed;

    public event EventHandler OnDialogueEnd;

    public void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogue();
        lastSpeed = player.moveSpeed;
        player.moveSpeed = 0;
        if(disableAfter)
            collisionEvent.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        dialogueText.text = string.Empty;
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if(index < lines.Length - 1) 
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            OnDialogueEnd?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
            player.moveSpeed = lastSpeed;
        }
    }
}

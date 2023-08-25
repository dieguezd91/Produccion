using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalScript : MonoBehaviour
{
    [SerializeField] GameObject dialogue;
    [SerializeField] Transform referencePoint;
    bool playerNearBy;

    void Update()
    {
        if (playerNearBy && Input.GetKeyDown(KeyCode.Space))
            dialogue.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerNearBy = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerNearBy = false;
    }
}

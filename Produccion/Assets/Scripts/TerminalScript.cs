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
        Debug.Log(playerNearBy);
        playerNearBy = Physics2D.OverlapBox(referencePoint.position, referencePoint.localScale, 0).CompareTag("Player");

        if (playerNearBy && Input.GetKeyDown(KeyCode.Space))
            dialogue.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(referencePoint.position, referencePoint.localScale);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    bool pjNearby;
    public GameObject menu;
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 1.5f).CompareTag("Player"))
        {
            pjNearby = true;
        }
        else pjNearby = false;

        if (pjNearby && Input.GetKeyDown(KeyCode.Space))
        {
            menu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1f);
    }
}

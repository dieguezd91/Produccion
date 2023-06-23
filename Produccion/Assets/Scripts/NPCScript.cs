using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    bool pjNearby;
    public GameObject menu;
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 3f).CompareTag("Player")) pjNearby = true;
        else pjNearby = false;

        if (pjNearby && Input.GetKeyDown(KeyCode.Space))
        {
            if(MenuManager.instance.menu.activeInHierarchy)
                MenuManager.instance.OpenCloseInventory();
            menu.SetActive(true);
            GameManager.instance.inStore = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(false);
            GameManager.instance.inStore = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 3f);
    }
}

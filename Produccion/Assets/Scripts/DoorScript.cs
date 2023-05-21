using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public string place;
    public bool pjNearby;
    public LayerMask Player;

    void Update()
    {
        pjNearby = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 1.05f), new Vector2(1f, 0.5f), 0f);
        Debug.Log(pjNearby);

        if (pjNearby && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(place);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 1.05f), new Vector2(1f, 0.5f));
    }
}

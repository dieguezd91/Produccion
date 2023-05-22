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
    public GameObject referencePoint;

    void Update()
    {
        pjNearby = Physics2D.OverlapBox(referencePoint.transform.position, new Vector2(2f, 0.5f), 0f);

        if (pjNearby && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(place);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(referencePoint.transform.position, new Vector2(2f, 0.5f));
    }
}

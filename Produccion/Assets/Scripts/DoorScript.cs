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

    void Update()
    {
        pjNearby = Physics2D.OverlapBox(transform.position, transform.localScale, 0f).CompareTag("Player");

        if (pjNearby)
        {
            SceneManagerScript.instance.LoadScene(place);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}

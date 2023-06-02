using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public string place;
    public bool pjNearby;
    public LayerMask layer;

    void Update()
    {
        pjNearby = Physics2D.OverlapBox(transform.position, transform.localScale, 0f, layer);

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

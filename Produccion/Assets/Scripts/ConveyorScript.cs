using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject belt;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            other.transform.position = Vector2.MoveTowards(other.transform.position, new Vector2(other.transform.position.x, endPoint.position.y), speed * Time.deltaTime);
    }
}

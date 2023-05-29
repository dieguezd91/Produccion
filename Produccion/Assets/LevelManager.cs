using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    //[SerializeField] Tilemap tilemap;

    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;

    
    void Start()
    {
        //bottomLeftEdge = tilemap.localBounds.min + new Vector3(0.5f, 1f, 0f);
        //topRightEdge = tilemap.localBounds.min + new Vector3(-0.5f, -1f, 0f);
        //PlayerController.instance.SetLimit(bottomLeftEdge, topRightEdge);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

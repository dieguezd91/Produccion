using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointScript : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnpoint;

    void Start()
    {
        //GameManager.instance.player.transform.position = spawnpoint.transform.position;
        Spawn();
    }

    public void Spawn()
    {
        player.transform.position = spawnpoint.transform.position;
    }
}

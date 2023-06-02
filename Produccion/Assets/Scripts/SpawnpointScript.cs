using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointScript : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnpoint;

    void Start()
    {
        player = GameManager.instance.player;
        Spawn();
    }

    public void Spawn()
    {
        player.transform.position = spawnpoint.transform.position;
    }
}

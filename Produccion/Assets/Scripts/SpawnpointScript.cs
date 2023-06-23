 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointScript : MonoBehaviour
{
    public GameObject player;
    public Transform spawnpoint;
    public static SpawnpointScript instance;

    void Start()
    {
        player = GameManager.instance.player;
        if (SceneManagerScript.instance.scene != "Ciudad") Spawn(spawnpoint.position);
        else Spawn(GameManager.instance.lastPosition);
    }

    public void Spawn(Vector3 spawnpoint)
    {
        player.transform.position = spawnpoint;
    }
}

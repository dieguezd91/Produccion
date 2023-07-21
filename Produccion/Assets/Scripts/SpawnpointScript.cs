 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointScript : MonoBehaviour
{
    public GameObject player;
    public Transform spawnpoint;
    public static SpawnpointScript instance;
    public Transform alternativeSpawn;

    void Start()
    {
        player = GameManager.instance.player;

        switch(SceneManagerScript.instance.scene)
        {
            case "Bar":
                if (GameManager.instance.tutorial) Spawn(alternativeSpawn.position);
                else Spawn(spawnpoint.position);
                break;
            case "Ciudad":
                Spawn(GameManager.instance.lastPosition);
                break;
            case "Garage":
                if (GameManager.instance.respawned)
                {
                    Spawn(alternativeSpawn.position);
                    GameManager.instance.respawned = false;
                }
                else Spawn(spawnpoint.position);
                break;
            default:
                Spawn(spawnpoint.position);
                break;
        }
    }

    public void Spawn(Vector3 spawnpoint)
    {
        player.transform.position = spawnpoint;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Transform playerSpawnPoint;
    public GameObject player;
    public static GameManager instance;
    public bool battleIsActive;

    [SerializeField] PlayerStats[] playerStats;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        playerStats = FindObjectsOfType<PlayerStats>();

        player = GameObject.FindGameObjectWithTag("Player");
        //player.transform.position = playerSpawnPoint.position;
    }

    void Update()
    {
        if(battleIsActive)
        {
            //Player.instance.deactivateMovement = true;
        }
        else
        {
            //Player.instance.deactivateMovement = false;
        }
    }

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
    }
}

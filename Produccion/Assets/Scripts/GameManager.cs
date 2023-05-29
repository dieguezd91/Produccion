using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform playerSpawnPoint;
    public GameObject player;
    [SerializeField] PlayerStats[] playerStats;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        playerStats = FindObjectsOfType<PlayerStats>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpawnPoint.position;
    }

    void Update()
    {
        
    }

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
    }
}

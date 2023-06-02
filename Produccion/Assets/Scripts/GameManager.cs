using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public static GameManager instance;
    public bool battleIsActive;
    public bool tutorial;

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

    public void RespawnPlayer()
    {
        SceneManagerScript.instance.LoadScene("Garage");
    }
}

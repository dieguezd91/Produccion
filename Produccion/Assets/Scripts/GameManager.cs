using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public static GameManager instance;
    public bool battleIsActive;
    public bool tutorial;
    public bool chatting;

    [SerializeField] PlayerStats[] playerStats;

    void Awake()
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

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
    }

    public void RespawnPlayer()
    {
        SceneManagerScript.instance.LoadScene("Garage");
        PlayerStats.instance.currentHP = PlayerStats.instance.maxHP;
    }
}

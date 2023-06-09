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
    public bool inStore;

    [SerializeField] PlayerStats[] playerStats;

    public Vector3 lastPosition;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
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
        SceneManagerScript.instance.scene = "Garage";
        lastPosition = new Vector2(-50f, 8.7f);
        SceneManagerScript.instance.LoadScene("Garage");
        player.transform.position = new Vector2(-6.88f, 2.95f);
        PlayerStats.instance.currentHP = PlayerStats.instance.maxHP;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;
    public string scene;

    public Vector2 spawnpoint;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void Update()
    {
        scene = SceneManager.GetActiveScene().name;

        //switch (scene)
        //{
        //    case "Bar":
        //        if (GameManager.instance.tutorial)
        //            spawnpoint = new Vector2(13.4f, 0.94f);
        //        else if (!GameManager.instance.tutorial)
        //            spawnpoint = new Vector2(-0.15f, 0.33f);
        //        break;
        //    case "Garage":
        //        spawnpoint = new Vector2(-6.83f, 0.58f);
        //        break;
        //    case "Ciudad":
        //        spawnpoint = new Vector2(-6.98f, 1.75f);
        //        break;
        //}
        //SpawnpointScript.Spawn(spawnpoint);
    }
    public void LoadScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

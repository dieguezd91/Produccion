using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class gameDataController : MonoBehaviour
{
    public GameObject player;

    public string saveArchives;
    public gameData gameData = new gameData();

    private void Awake()
    {
        saveArchives = Application.dataPath + "/gameData.json";
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.C)) LoadData();
    //    if (Input.GetKeyDown(KeyCode.G)) SaveData();
    //}

    public void LoadData()
    {
        if (File.Exists(saveArchives))
        {
            string content = File.ReadAllText(saveArchives);
            gameData = JsonUtility.FromJson<gameData>(content);

            Debug.Log("Player's position: " + gameData.position);

            player.transform.position = gameData.position;
        }
        else
        {
            Debug.Log("El archivo no existe");
        }
    }

    public void SaveData()
    {
        gameData newData = new gameData()
        {
            position = player.transform.position
        };

        string JSONchain = JsonUtility.ToJson(newData);
        File.WriteAllText(saveArchives, JSONchain);

        Debug.Log("Data saved");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;
    public string scene;

    public Vector2 spawnpoint;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void LoadScene(string newScene)
    {
        MusicManager.instance.audioSource.Stop();
        scene = newScene;
        CheckActiveClip();
        MusicManager.instance.audioSource.clip = MusicManager.instance.activeClip;
        MusicManager.instance.audioSource.Play();
        if (newScene == "Fabrica" || newScene == "Central de seguridad")
            AudioManager.instance.lockedUpSFX.enabled = true;
        else AudioManager.instance.lockedUpSFX.enabled = false;
        SceneManager.LoadScene(newScene);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("I'm outta here!");
    }
    void CheckActiveClip()
    {
        Debug.Log("Check");
        switch (scene)
        {
            case "MainMenu":
                MusicManager.instance.activeClip = MusicManager.instance.songs[0];
                break;
            case "Bar":
                MusicManager.instance.activeClip = MusicManager.instance.songs[1];
                break;
            case "Garage":
                MusicManager.instance.activeClip = MusicManager.instance.songs[1];
                break;
            case "Ciudad":
                MusicManager.instance.activeClip = MusicManager.instance.songs[0];
                break;
            case "Store":
                MusicManager.instance.activeClip = MusicManager.instance.songs[0];
                break;
            case "Fabrica":
                MusicManager.instance.activeClip = MusicManager.instance.songs[2];
                break;
            default:
                break;
        }
    }
}

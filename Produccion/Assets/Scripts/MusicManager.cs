using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    MusicManager instance;
    AudioSource audioSource;
    AudioClip activeClip;
    [SerializeField] AudioClip[] songs;

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
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.Play();
    }

    private void Update()
    {
        CheckActiveClip();
    }

    void CheckActiveClip()
    {
        switch (SceneManagerScript.instance.scene)
        {
            case "MainMenu":
                activeClip = songs[0];
                break;
            case "Bar":
                activeClip = songs[1];
                break;
            case "Garage":
                activeClip = songs[1];
                break;
            case "Ciudad":
                activeClip = songs[2];
                break;
            case "Store":
                activeClip = songs[1];
                break;
            case "Fabrica":
                activeClip = songs[3];
                break;
            default:
                break;
        }
        audioSource.clip = activeClip;
    }
}
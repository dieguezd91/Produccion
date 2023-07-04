using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    MusicManager instance;
    AudioSource audioSource;
    AudioClip activeClip;
    AudioClip[] songs;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this && instance != null) Destroy(this);
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        switch(SceneManagerScript.instance.scene)
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
        }
        audioSource.clip = activeClip;
        audioSource.Play();
    }
}

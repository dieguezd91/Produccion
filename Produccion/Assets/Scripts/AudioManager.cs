using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioListener audioListener;

    public AudioSource audioSource;

    //[SerializeField] AudioClip clip;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else if(instance == null) instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        audioListener = BattleManager.instance.worldAudioListener;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}

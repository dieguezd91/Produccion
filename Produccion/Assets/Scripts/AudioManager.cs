using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSource;
    public AudioSource lockedUpSFX;
    float volume;
    public Slider volumeSlider;
    [SerializeField] AudioClip pistolSFX;
    [SerializeField] AudioClip SMGSFX;
    [SerializeField] AudioClip shotgunSFX;

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
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }

    private void Update()
    {
        if(volumeSlider != null)
        {
            audioSource.volume = volumeSlider.value;
            volume = volumeSlider.value;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void selectRangeAttackSFX(ItemsManager weapon)
    {
        AudioClip clip;
        switch (weapon.itemName)
        {
            case "Pistola":
                clip = pistolSFX;
                PlaySound(clip);
                break;
            case "Subfusil":
                clip = SMGSFX;
                PlaySound(clip);
                break;
            case "Escopeta":
                clip = shotgunSFX;
                PlaySound(clip);
                break;
            default:
                Debug.Log("Error rangeSFX");
                break;
        }
    }
}

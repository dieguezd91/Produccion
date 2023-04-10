using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    private AudioSource source;
    public AudioClip clip;
    public int volume;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISingleton : MonoBehaviour
{
    public static UISingleton instance;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

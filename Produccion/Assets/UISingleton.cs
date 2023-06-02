using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISingleton : MonoBehaviour
{
    public static UISingleton instance;

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
        DontDestroyOnLoad(gameObject);
    }
}

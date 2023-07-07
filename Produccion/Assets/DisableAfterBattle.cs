using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterBattle : MonoBehaviour
{
    void Start()
    {
        BattleManager.instance.OnBattleEnd += Deactivate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Deactivate(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }
}

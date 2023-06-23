using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomBattle : MonoBehaviour
{
    Collider2D colider;
    private void Start()
    {
        colider = GetComponent<Collider2D>();
        BattleManager.instance.OnBattleEnd += Scape;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            int i = UnityEngine.Random.Range(1, 101);
            string enemy;
            if (i <= 5) enemy = "Mercenario";
            else enemy = "Maton";

            if ( i <= 10)
            {
                Debug.Log("Combate random!");
                BattleManager.instance.StartBattle(null, enemy, true);
            }
        }
    }

    void Scape( object sender, EventArgs e)
    {
        StartCoroutine(Inmunity());
    }

    IEnumerator Inmunity()
    {
        colider.enabled = false;
        yield return new WaitForSeconds(3f);
        colider.enabled = true;
    }
}
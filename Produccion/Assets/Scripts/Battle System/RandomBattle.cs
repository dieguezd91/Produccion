using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomBattle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            int i = Random.Range(1, 101);
            string enemy;
            if (i <= 5) enemy = "Mercenario";
            else enemy = "Maton";

            if ( i <= 10)
            {
                BattleManager.instance.StartBattle(null, enemy, true);
            }
        }
    }
}
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
            Debug.Log("Colision");

            int i = Random.Range(1, 101);
            Debug.Log(i);
            string enemy;
            if (i <= 5) enemy = "Mercenario";
            else enemy = "Maton";

            if ( i <= 10)
            {
                Debug.Log("Combate");
                BattleManager.instance.StartBattle(null, enemy);
            }
        }
    }
}
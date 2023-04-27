using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<GameObject> itemPrefabs;
    CharacterStats charStats;

    private void Start()
    {
        charStats = GetComponent<CharacterStats>();
    }

    public void OnDestroy()
    {
        if (itemPrefabs.Count > 0)
        {
             // Choose a random item from the list
            int randomIndex = Random.Range(0, itemPrefabs.Count);
            GameObject randomItem = itemPrefabs[randomIndex];

            // Spawn the item at the position of the destroyed enemy
            Instantiate(randomItem, transform.position, Quaternion.identity);
        }

        charStats.Exp += 20;
    }
}

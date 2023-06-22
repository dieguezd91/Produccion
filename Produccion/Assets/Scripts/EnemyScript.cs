using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public string names;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameManager.instance.player.transform.position.y < transform.position.y) spriteRenderer.sortingOrder = -1;
        else spriteRenderer.sortingOrder = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BattleManager.instance.StartBattle(this.gameObject, names, false);
        }
    }
}

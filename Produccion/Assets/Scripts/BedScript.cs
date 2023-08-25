using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedScript : MonoBehaviour
{
    PlayerStats player;
    [SerializeField] GameObject HPRestoredSign;
    public bool pjNearBy;

    private void Start()
    {
        player = GameManager.instance.player.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        pjNearBy = Physics2D.OverlapBox(transform.position, transform.localScale, 0f).CompareTag("Player");

        if(pjNearBy && Input.GetKeyDown(KeyCode.Space))
        {
            player.currentHP = player.maxHP;
            StartCoroutine(ShowSign());
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector2(2.5f, 3f));
    }

    private IEnumerator ShowSign()
    {
        HPRestoredSign.SetActive(true);
        yield return new WaitForSeconds(2f);
        HPRestoredSign.SetActive(false);
    }
}

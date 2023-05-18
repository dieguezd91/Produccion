using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float horizontal;
    private float vertical;
    Rigidbody2D rb2D;


    public CharacterStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<CharacterStats>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // MOVIMIENTO
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.position += movement.normalized * moveSpeed * Time.deltaTime;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed;
    public bool isMoving;
    private Vector2 input;
    private Rigidbody2D rb;

    private Animator animator;

    private void Awake()
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

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        // MOVIMIENTO
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        input = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * moveSpeed * Time.deltaTime);
    }

}

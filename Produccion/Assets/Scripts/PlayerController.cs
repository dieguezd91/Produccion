using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject worldCamera;

    public float moveSpeed;
    public bool isMoving;
    public Vector2 input;
    private Rigidbody2D rb;

    public LayerMask enemiesLayer;

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

        if (!GameManager.instance.chatting)
        {
            // ANIMACIONES
            animator.SetFloat("Horizontal", input.x);
            animator.SetFloat("Vertical", input.y);
            animator.SetFloat("Speed", input.sqrMagnitude);

        }
        
        CheckForEncounters();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * moveSpeed * Time.deltaTime);
    }

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, enemiesLayer) != null)
        {
            if(UnityEngine.Random.Range(1, 101) <= 10)
            {
                Debug.Log("Un enemigo te ataca");
            }
        }
    }

}

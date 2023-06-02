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
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input = new Vector2(input.x, input.y).normalized;

        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);

        /*
         * if(input != Vector2.zero)
        {
            rb.MovePosition(new Vector2(transform.position.x + input.x * moveSpeed * Time.deltaTime,
<<<<<<< Updated upstream
            transform.position.y + input.y * moveSpeed * Time.deltaTime));
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
=======
                                        transform.position.y + input.y * moveSpeed * Time.deltaTime));

            isMoving = true;
>>>>>>> Stashed changes
        }
        else
        { 
            animator.SetFloat("moveX", input.x);
<<<<<<< Updated upstream
            animator.SetFloat("moveY", input.y);        
            isMoving = false;
        }
        */
        animator.SetFloat("moveY", input.y);
        animator.SetBool("isMoving", isMoving);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * moveSpeed * Time.fixedDeltaTime);
    }
    /*IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
<<<<<<< Updated upstream
    }*/

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        BattleManager.instance.StartBattle(new string[] { "Patrol" });
    //    }
    //}
}

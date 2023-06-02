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
        
        if(input != Vector2.zero)
        {
            rb.MovePosition(new Vector2(transform.position.x + input.x * moveSpeed * Time.deltaTime,
                                            transform.position.y + input.y * moveSpeed * Time.deltaTime));
        }
        else
            isMoving = false;


        if (!isMoving)
        {

            //remueve el movimiento en diagonal
            //if (input.x != 0) input.y = 0;

            //var targetPos = transform.position;
            //targetPos.x += input.x;
            //targetPos.y += input.y;
            //StartCoroutine(Move(targetPos));

            animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
        }
        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        BattleManager.instance.StartBattle(new string[] { "Patrol" });
    //    }
    //}
}

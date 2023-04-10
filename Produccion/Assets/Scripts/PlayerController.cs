using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D rb2D;
    private Vector2 moveInput;
    //private Animator animator;

    private UIManager manager;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //NORMALIZAMOS EL VECTOR EN DIAGONAL PARA QUE SE MUEVA A LA MISMA VELOCIDAD QUE EN HORIZONTAL/VERTICAL
        moveInput = new Vector2(moveX, moveY).normalized;

        //animator.SetFloat("Horizontal", moveX);
        //animator.SetFloat("Vertical", moveY);
        //animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Escape)) manager.OpenPauseMenu();
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + moveInput * speed * Time.fixedDeltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D rb2D;
    private Vector2 moveInput;
    public bool walksRight = true;
    //private Animator animator;

    [SerializeField] private Transform shootController;
    [SerializeField] private GameObject bullet;

    public UIManager manager;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //(moveX, moveY) * Time.deltaTime;

        //NORMALIZAMOS EL VECTOR EN DIAGONAL PARA QUE SE MUEVA A LA MISMA VELOCIDAD QUE EN HORIZONTAL/VERTICAL
        moveInput = new Vector2(moveX, moveY).normalized;

        //animator.SetFloat("Horizontal", moveX);
        //animator.SetFloat("Vertical", moveY);
        //animator.SetFloat("Speed", moveInput.sqrMagnitude);

        //DISPARO
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) manager.OpenPauseMenu();
    }
    

    private void Shooting()
    {
        if (walksRight)
        {
            Instantiate(bullet, shootController.position, Quaternion.Euler(0, 0, 0));
        }
        else if (!walksRight)
        {
            Instantiate(bullet, shootController.position, Quaternion.Euler(0, 0, -180));
        }
    }

    private void Walk(float walk)
    {
        rb2D.MovePosition(rb2D.position + moveInput * speed * Time.fixedDeltaTime);

        //GIRO DE PERSONAJE
        if (walk > 0 && !walksRight)
        {
            Flip();
        }
        else if (walk < 0 && walksRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        walksRight = !walksRight;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

}

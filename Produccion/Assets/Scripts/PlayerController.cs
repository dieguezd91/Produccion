using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D rb2D;
    private Vector2 moveInput;

    [SerializeField] private Transform shootController;
    [SerializeField] private GameObject bullet;

    private UIManager manager;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //NORMALIZAMOS EL VECTOR EN DIAGONAL PARA QUE SE MUEVA A LA MISMA VELOCIDAD QUE EN HORIZONTAL/VERTICAL
        moveInput = new Vector2(horizontal, vertical).normalized;

        rb2D.MovePosition(rb2D.position + moveInput * speed * Time.deltaTime);

        
            //DISPARO
            if (Input.GetButtonDown("Fire1"))
        {
            Shooting(); 
        }

        if (Input.GetKeyDown(KeyCode.Escape)) manager.OpenPauseMenu();
    }

    private void Shooting()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }    

}

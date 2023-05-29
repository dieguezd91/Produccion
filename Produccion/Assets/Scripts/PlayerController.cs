using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
<<<<<<< Updated upstream
    public float moveSpeed = 5f;
    private float horizontal;
    private float vertical;
    Rigidbody2D rb2D;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public CharacterStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<CharacterStats>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
=======
    public float moveSpeed;
    public LayerMask battleLayer;

    public event Action OnEncountered;

    public bool isMoving;
    private Vector2 input;

    //public CharacterStats playerStats;
    private Animator animator;

    private void Awake()
    {
        //playerStats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
>>>>>>> Stashed changes
    {
        // MOVIMIENTO
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.position += movement.normalized * moveSpeed * Time.deltaTime;

        // APUNTADO Y DISPARO
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;
            direction.z = 0f;
            direction.Normalize();

<<<<<<< Updated upstream
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet.GetComponent<Bullet>().speed;
=======
            //remueve el movimiento en diagonal
            //if (input.x != 0) input.y = 0;

            if(input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                StartCoroutine(Move(targetPos));
            }
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

        CheckForEncounters();
    }

    private void CheckForEncounters()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.2f, battleLayer) !=null)
        {
            if (UnityEngine.Random.Range(1, 101) <= 10)
            {
                animator.SetBool("isMoving", false);
                OnEncountered();
                Debug.Log("battle");
            }
>>>>>>> Stashed changes
        }
    }
}

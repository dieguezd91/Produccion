using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet.GetComponent<Bullet>().speed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Update()
    {
        // MOVIMIENTO
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
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

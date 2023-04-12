using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // CALCULA LA DISTANCIA ENTRE EL PLAYER Y EL ENEMIGO
            Vector3 direction = transform.position - collision.transform.position;
            direction.z = 0f;
            direction.Normalize();
        }

        //DESTRUIR LA BALA
        Destroy(gameObject);
    }
}


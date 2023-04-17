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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Salud>().GetDamage();

            // CALCULA LA DISTANCIA ENTRE EL PLAYER Y EL ENEMIGO
            Vector3 direction = transform.position - collision.transform.position;
            direction.z = 0f;
            direction.Normalize();
        }

        if (collision.gameObject.CompareTag("Destroyable"))
        {
            collision.gameObject.GetComponent<Salud>().GetDamage();
        }

            //DESTRUIR LA BALA
            Destroy(gameObject);
    }
}


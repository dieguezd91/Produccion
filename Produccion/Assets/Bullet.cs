using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float velocity;

    [SerializeField] private float damage;

    
    private void Update()
    {
        transform.Translate(Vector2.up * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //collision.GetComponent<Enemy>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}

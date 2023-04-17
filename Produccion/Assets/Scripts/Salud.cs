using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salud : MonoBehaviour
{
    public float HP;
    public float maxHP;
    public float damage;
    Enemy enemy;

    private void Start()
    {
        HP = maxHP;
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if(HP > maxHP)
        {
            HP = maxHP;
        }
    }

    public void GetDamage()
    {
        HP -= damage;
        if (HP <= 0)
        {
            enemy.OnDestroy();
            Destroy(gameObject);
        }
    }


}

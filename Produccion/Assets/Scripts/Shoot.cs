using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform shootController;

    [SerializeField] private GameObject bullet;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }
    }

    private void Shooting()
    {
        Instantiate(bullet, shootController.position, shootController.rotation);
    }
}

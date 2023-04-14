using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject inv;
    public bool Activar_inv;

    public GameObject Selector;
    public int ID;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Activar_inv)
        {
            inv.SetActive(true);
        }
        else
        {
            inv.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Activar_inv = !Activar_inv;
        }

        Navigate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                if (Bag[i].GetComponent<Image>().enabled == false)
                {
                    Bag[i].GetComponent<Image>().enabled = true;
                    Bag[i].GetComponent<Image>().sprite = collision.GetComponent<SpriteRenderer>().sprite;
                    break;
                }
            }
        }
    }

    public void Navigate()
    {
        if (Input.GetKeyDown(KeyCode.D) && ID < Bag.Count - 1)
        {
            ID++;
        }
        if (Input.GetKeyDown(KeyCode.A) && ID > 0)
        {
            ID--;
        }
        if (Input.GetKeyDown(KeyCode.W) && ID > 2)
        {
            ID -=3;
        }
        if (Input.GetKeyDown(KeyCode.S) && ID < 6)
        {
            ID +=3;
        }

        Selector.transform.position = Bag[ID].transform.position;

    }
}

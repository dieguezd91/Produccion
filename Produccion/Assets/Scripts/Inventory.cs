using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Bag = new List<GameObject>();
    public GameObject[] inv;
    public bool Activar_inv;

    public GameObject Selector;
    public int ID;

    public List<Image> Equipo = new List<Image>();
    public int ID_equipo;
    public int Fases_inv;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.V))
        {
            Activar_inv = !Activar_inv;
        }

        Navigate();

        if (Activar_inv)
        {
            inv[0].SetActive(true);
        }
        else
        {
            Fases_inv = 0;
            inv[0].SetActive(false);
        }
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
            Destroy(collision.gameObject);
        }
    }

    public void Navigate()
    {
        switch(Fases_inv)
        {
            case 0:

                inv[1].SetActive(false);

                if(Input.GetKeyDown(KeyCode.W) && ID_equipo > 0)
                {
                    ID_equipo--;
                }

                if (Input.GetKeyDown(KeyCode.S) && ID_equipo < Equipo.Count-1)
                {
                    ID_equipo++;
                }

                Selector.transform.position = Equipo[ID_equipo].transform.position;

                if(Input.GetKeyDown(KeyCode.F) && Activar_inv)
                {
                    Fases_inv = 1;
                }

                break;

            case 1:

                inv[1].SetActive(true);

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
                    ID -= 3;
                }
                if (Input.GetKeyDown(KeyCode.S) && ID < 6)
                {
                    ID += 3;
                }

                Selector.transform.position = Bag[ID].transform.position;

                if(Input.GetKeyDown(KeyCode.G) && Activar_inv)
                {
                    Fases_inv = 0;
                }

                break;
        }
        

    }
}

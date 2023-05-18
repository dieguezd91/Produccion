using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public string place;
    bool pjNearby;

    void Update()
    {
        pjNearby = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1f), Vector2.down);

        if(pjNearby && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(place);
        }
    }
}

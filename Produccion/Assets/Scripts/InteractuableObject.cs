using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuableObject : MonoBehaviour
{
    public float radius = 3f;

    bool isFocus = false;
    Transform player;

    
    void Update()
    {
        if (isFocus)
        {
            float distance = Vector2.Distance(player.position, transform.position);
            if(distance <= radius)
            {
                Debug.Log("interactua :)");
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

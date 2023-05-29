using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Start combat encounter
            StartCombat();
        }
    }

    void StartCombat()
    {
        Debug.Log("Combate iniciado");
        // Set the inCombat flag to true
        Camera.main.GetComponent<CameraController>().inCombat = true;

        // Play combat music or sound effect

        // Load the combat scene or activate a combat UI
        SceneManager.LoadScene("CombatScene");
        
    }
}

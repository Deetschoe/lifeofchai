using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Include the scene management namespace

public class TeleportToNewScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Load the "Castle" scene
            SceneManager.LoadScene("Castle");
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToNextScene2 : MonoBehaviour
{
    // Name of the scene you want to load
    public string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

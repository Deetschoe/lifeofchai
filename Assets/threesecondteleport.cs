using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeSecondTeleport : MonoBehaviour
{
    // Name of the scene you want to load
    public string nextSceneName = "village"; // Change nextSceneName to "village"
    private bool playerEntered = false;
    private float teleportTimer = 0f;
    private float teleportDelay = 3f;

    private void Update()
    {
        if (playerEntered)
        {
            teleportTimer += Time.deltaTime;
            if (teleportTimer >= teleportDelay)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            playerEntered = true;
        }
    }
}

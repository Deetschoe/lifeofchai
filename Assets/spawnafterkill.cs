using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public GameObject objectPrefab; // Assign the prefab of the object to respawn in the Inspector
    public Transform respawnPosition; // Assign the transform at which to respawn the object. Could be an empty GameObject in the scene
    private GameObject currentObjectInstance;

    private void Start()
    {
        // Spawn the initial instance of the object
        if (objectPrefab != null)
        {
            currentObjectInstance = Instantiate(objectPrefab, respawnPosition.position, respawnPosition.rotation);
        }
    }

    private void Update()
    {
        // Check if the current instance has been destroyed
        if (currentObjectInstance == null)
        {
            // Wait for a certain delay if necessary (optional)
            // StartCoroutine(RespawnAfterDelay(2.0f)); // Example delay respawn. Uncomment if you wish to use a delay.

            // Respawn immediately
            RespawnObject();
        }
    }

    private void RespawnObject()
    {
        if (objectPrefab != null)
        {
            currentObjectInstance = Instantiate(objectPrefab, respawnPosition.position, respawnPosition.rotation);
        }
    }

    // Optional: Implement a delay before respawning
    /*
    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        RespawnObject();
    }
    */
}

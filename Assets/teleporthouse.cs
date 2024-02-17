using UnityEngine;

public class TeleportToTarget : MonoBehaviour
{
    // The target empty GameObject to teleport to
    public Transform teleportTarget;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the specified target
            other.transform.position = teleportTarget.position;
        }
    }
}

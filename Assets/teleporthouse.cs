using UnityEngine;

public class TeleportToTarget : MonoBehaviour
{
    // The target empty GameObject to teleport to
    public Transform teleportTarget;

    // Array to hold your AudioSource components
    public AudioSource[] audioSources;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger."); // Debug statement

            // Teleport the player to the specified target
            other.transform.position = teleportTarget.position;

            // Play a random audio clip from the audioSources array
            if (audioSources.Length > 0)
            {
                int randomIndex = Random.Range(0, audioSources.Length);
                if (!audioSources[randomIndex].isPlaying)
                {
                    audioSources[randomIndex].Play();
                    Debug.Log($"Playing audio from source {randomIndex}."); // Debug statement
                }
                else
                {
                    Debug.Log($"Audio source {randomIndex} is already playing."); // Debug statement
                }
            }
            else
            {
                Debug.Log("No audio sources assigned or available."); // Debug statement
            }
        }
    }
}

using UnityEngine;

public class PlayAudioInRoom : MonoBehaviour
{
    public AudioSource audioSource; // Assign in the Inspector

    void Start()
    {
        // Check if an AudioSource has been assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not assigned. Please assign one in the inspector.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the GameObject entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Play the audio clip if it's not already playing and an AudioSource has been assigned
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the GameObject exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Stop the audio clip if it's playing
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}

using UnityEngine;

public class BirdAudioTrigger : MonoBehaviour
{
    public AudioClip clip; // Assign this in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = clip;
        audioSource.playOnAwake = false; // Ensure the clip doesn't play automatically on awake
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Play the audio clip if it's not already playing
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    // Optional: Stop playing the audio when the player leaves the collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

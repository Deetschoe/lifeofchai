using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Try to get the AudioSource component attached to this GameObject.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If no AudioSource is found, log an error.
            Debug.LogError("AudioSource component missing from this GameObject. Please attach one.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the GameObject entering the trigger is tagged as "Player".
        if (other.CompareTag("Player"))
        {
            // Play the audio clip if it's not already playing.
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}

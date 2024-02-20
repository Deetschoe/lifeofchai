using UnityEngine;

public class PlayAudioInRoom2 : MonoBehaviour
{
    public AudioSource audioSource1; // Assign the first AudioSource in the Inspector
    public AudioSource audioSource2; // Assign the second AudioSource in the Inspector

    void Start()
    {
        // Check if AudioSource components have been assigned
        if (audioSource1 == null || audioSource2 == null)
        {
            Debug.LogError("One or both AudioSource components not assigned. Please assign them in the inspector.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the GameObject entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Play the first audio clip if it's not already playing and an AudioSource has been assigned
            if (audioSource1 != null && !audioSource1.isPlaying)
            {
                audioSource1.Play();
            }
            // Similarly, handle the second audio source
            if (audioSource2 != null && !audioSource2.isPlaying)
            {
                audioSource2.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the GameObject exiting the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Stop the first audio clip if it's playing
            if (audioSource1 != null && audioSource1.isPlaying)
            {
                audioSource1.Stop();
            }
            // Similarly, handle the second audio source
            if (audioSource2 != null && audioSource2.isPlaying)
            {
                audioSource2.Stop();
            }
        }
    }
}

using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip clip1; // Assign this in the Inspector for the first audio clip
    public AudioClip clip2; // Assign this in the Inspector for the second audio clip
    private AudioSource audioSource;

    private void Start()
    {
        // Ensure there is an AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true; // Make the music loop
        }

        // Set the AudioSource not to play on awake and initially assign clip1
        audioSource.clip = clip1;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as Player
        if (other.CompareTag("Player"))
        {
            // Check which clip is currently assigned and play or switch accordingly
            if (audioSource.clip == clip1 && !audioSource.isPlaying)
            {
                audioSource.Play(); // Play clip1 if it's not already playing
            }
            else if (audioSource.clip != clip1)
            {
                audioSource.clip = clip1; // Switch to clip1 if clip2 was assigned
                audioSource.Play(); // And play clip1
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is tagged as Player and clip1 is playing
        if (other.CompareTag("Player") && audioSource.clip == clip1)
        {
            // Switch to clip2 when the player exits the collider
            audioSource.clip = clip2;
            audioSource.Play(); // Play clip2
        }
    }
}

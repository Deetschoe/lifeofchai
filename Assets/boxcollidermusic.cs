using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip clip1; // Assign this in the Inspector for the first audio clip
    public AudioClip clip2; // Assign this in the Inspector for the second audio clip
    private AudioSource audioSource1;
    private AudioSource audioSource2;

    private void Start()
    {
        // Ensure there are AudioSource components attached to this GameObject
        // For AudioSource1
        audioSource1 = GetComponent<AudioSource>();
        if (audioSource1 == null)
        {
            audioSource1 = gameObject.AddComponent<AudioSource>();
            audioSource1.loop = true; // Make the first music loop
        }

        // Assign the first clip to the AudioSource1 and set it not to play on awake
        audioSource1.clip = clip1;
        audioSource1.playOnAwake = false;

        // For AudioSource2 - add a new AudioSource for the second clip
        audioSource2 = gameObject.AddComponent<AudioSource>(); // Adding a new AudioSource component
        audioSource2.clip = clip2;
        audioSource2.loop = true; // Option to make the second music loop
        audioSource2.playOnAwake = false; // Ensure it doesn't play on awake
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as Player
        if (other.CompareTag("Player"))
        {
            // Play or resume the music from both audio sources
            if (!audioSource1.isPlaying)
            {
                audioSource1.Play();
            }
            if (!audioSource2.isPlaying)
            {
                audioSource2.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is tagged as Player
        if (other.CompareTag("Player"))
        {
            // Pause the music from both audio sources
            if (audioSource1.isPlaying)
            {
                audioSource1.Pause();
            }
            if (audioSource2.isPlaying)
            {
                audioSource2.Pause();
            }
        }
    }
}

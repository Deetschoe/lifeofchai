using UnityEngine;

public class PlayAudioDelayed : MonoBehaviour
{
    public AudioSource audioSource; // Assign your AudioSource in the Inspector.

    private float delay = 5.0f; // Delay in seconds before playing the audio.
    private bool hasPlayed = false;

    private void Start()
    {
        // Ensure the AudioSource is assigned.
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned! Please assign an AudioSource.");
            return;
        }

        // Disable the AudioSource initially.
        audioSource.Stop();
    }

    private void Update()
    {
        if (!hasPlayed)
        {
            // Wait for the specified delay before playing the audio.
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                // Play the audio after the delay.
                audioSource.Play();
                hasPlayed = true;
            }
        }
    }
}

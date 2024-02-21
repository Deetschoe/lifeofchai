using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseVolume : MonoBehaviour
{
    public float fadeDuration = 30f; // Time in seconds to fade the volume

    private AudioSource audioSource;
    private float initialVolume;
    private float timer;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if AudioSource is present
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject!");
            return;
        }

        // Save the initial volume
        initialVolume = audioSource.volume;

        // Initialize the timer
        timer = fadeDuration;
    }

    void Update()
    {
        // Check if AudioSource is present
        if (audioSource == null)
        {
            return;
        }

        // Update the timer
        timer -= Time.deltaTime;

        // Calculate the new volume based on the timer
        float newVolume = Mathf.Lerp(0f, initialVolume, timer / fadeDuration);

        // Set the new volume to the AudioSource
        audioSource.volume = newVolume;

        // Check if the fading is complete
        if (timer <= 0f)
        {
            // Optionally, you can stop or disable the AudioSource here
            // audioSource.Stop();
            // audioSource.enabled = false;

            // Destroy the script component if you want it to stop updating
            Destroy(this);
        }
    }
}

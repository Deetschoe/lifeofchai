using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeAttackSounds : MonoBehaviour
{
    public AudioClip[] randomAudioClips;
    public float proximityThreshold = 5f;

    private AudioSource audioSource;
    private bool isPlaying = false;

    void Start()
    {
        // Ensure there is an AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Start playing audio when the script is first initialized
        PlayRandomAudio();
    }

    void Update()
    {
        // Check for nearby "Enemy" objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // Check proximity to the enemy
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < proximityThreshold && !isPlaying)
            {
                // Play random audio clip on the enemy
                PlayRandomAudio();

                // Set a flag to prevent playing audio until the current clip finishes
                isPlaying = true;

                // Invoke a method to reset the flag after the audio clip duration
                float clipDuration = audioSource.clip.length;
                Invoke("ResetIsPlaying", clipDuration);
            }
        }
    }

    void PlayRandomAudio()
    {
        // Pick a random audio clip from the array
        int randomIndex = Random.Range(0, randomAudioClips.Length);
        AudioClip randomClip = randomAudioClips[randomIndex];

        // Assign the chosen clip to the AudioSource
        audioSource.clip = randomClip;

        // Play the audio
        audioSource.Play();
    }

    void ResetIsPlaying()
    {
        // Reset the flag to allow playing audio again
        isPlaying = false;
    }
}

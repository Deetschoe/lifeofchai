﻿using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    // Array to hold different sound clips
    public AudioClip[] soundClips;

    private bool isPouring = false;
    private Stream currentStream = null;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    private void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() > pourThreshold;

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
    }

    private void StartPour()
    {
        print("Start");

        currentStream = CreateStream();
        currentStream.Begin();

        // Play a random audio clip
        PlayRandomSound();

        // Optionally adjust the volume right when starting to pour
        AdjustVolume(0.5f); // Example: Set volume to 50%
    }

    private void EndPour()
    {
        print("End");

        currentStream.End();
        currentStream = null;

        // Stop the audio when pouring ends
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private float CalculatePourAngle()
    {
        return transform.up.y * Mathf.Rad2Deg;
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }

    private void PlayRandomSound()
    {
        if (audioSource != null && soundClips.Length > 0)
        {
            // Select a random AudioClip from the array
            AudioClip clip = soundClips[Random.Range(0, soundClips.Length)];
            audioSource.clip = clip;

            // Play the selected AudioClip
            audioSource.Play();
        }
    }

    // Public method to adjust the volume
    public void AdjustVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp(volume, 0f, 0.7f); // Clamp the volume between 0 and 1
        }
    }
}

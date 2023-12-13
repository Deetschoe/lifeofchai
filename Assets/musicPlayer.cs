using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour
{
    public List<AudioClip> audioClips;          // List of audio clips to play
    public AudioClip transitionClip;            // Audio clip to play between songs
    public AudioSource audioSource;             // AudioSource component
    public GameObject leftHand;                 // Left Hand GameObject
    public GameObject rightHand;                // Right Hand GameObject
    public GameObject nextObject;               // Object to trigger the next song
    public GameObject stopObject;               // Object to stop all music
    public GameObject volumeUp;                 // Object to increase volume
    public GameObject volumeDown;               // Object to decrease volume
    public AudioClip volumeChangeClip;          // Sound to play when volume changes

    private float proximityThreshold = 0.15f;   // Proximity threshold for hand to object
    private float cooldown = 4.0f;              // Cooldown duration in seconds
    private float lastInteractionTime = 0;      // Time when the last interaction occurred
    private bool stopAllMusic = false;          // Flag to stop all music
    private float volume = 0.128f;                // Current volume level

    void Start()
    {
        StartCoroutine(PlayAudioSequence());
    }

    IEnumerator PlayAudioSequence()
    {
        int currentClipIndex = 0; // Current index of the clip being played

        while (!stopAllMusic) // Loop until stopAllMusic is true
        {
            // Play transition clip between songs
            if (transitionClip != null)
            {
                audioSource.clip = transitionClip;
                audioSource.Play();
                yield return new WaitForSeconds(transitionClip.length);
            }

            // Select the current clip from the list
            AudioClip currentClip = audioClips[currentClipIndex];
            audioSource.clip = currentClip;
            audioSource.Play();

            float playTime = 0;
            while (playTime < currentClip.length && !stopAllMusic)
            {
                playTime += Time.deltaTime;

                // Check proximity for volume control and cooldown
                if (Time.time - lastInteractionTime > cooldown)
                {
                    if (IsHandCloseToObject(volumeUp))
                    {
                        print("this code runs");
                        AdjustVolume(true);
                    }
                    else if (IsHandCloseToObject(volumeDown))
                    {
                        print("this code runs2");

                        AdjustVolume(false);
                    }
                }

                // Check proximity for next song and cooldown
                if (Time.time - lastInteractionTime > cooldown &&
                    IsHandCloseToObject(nextObject))
                {
                    lastInteractionTime = Time.time;
                    break; // Exit the current song
                }

                // Check proximity for stopping all music
                if (IsHandCloseToObject(stopObject))
                {
                    StopAllMusic();
                }

                yield return null;
            }

            // Move to the next song or loop back to the start
            currentClipIndex = (currentClipIndex + 1) % audioClips.Count;
        }
    }

    void AdjustVolume(bool increase)
    {
        lastInteractionTime = Time.time;

        // Play volume change sound
        if (volumeChangeClip != null)
        {
            AudioSource.PlayClipAtPoint(volumeChangeClip, transform.position);
        }

        // Adjust volume
        volume += increase ? 0.1f : -0.1f;
        volume = Mathf.Clamp(volume, 0.0f, 1.0f);
        audioSource.volume = volume;
        Debug.Log("New Volume: " + volume);

    }

    bool IsHandCloseToObject(GameObject obj)
    {
        return Vector3.Distance(leftHand.transform.position, obj.transform.position) < proximityThreshold ||
               Vector3.Distance(rightHand.transform.position, obj.transform.position) < proximityThreshold;
    }

    void StopAllMusic()
    {
        audioSource.Stop(); // Stop the current playing clip
        stopAllMusic = true;
    }
}

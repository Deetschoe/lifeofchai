using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips;   // List of audio clips to play
    public AudioSource audioSource;      // AudioSource component
    public AudioClip stopClip;           // Audio clip to play when stopped
    public GameObject leftHand;          // Left Hand GameObject
    public GameObject rightHand;         // Right Hand GameObject
    public GameObject stopButton;        // Stop Button GameObject

    private float proximityThreshold = 0.15f; // Proximity threshold for hand to button

    void Start()
    {
        StartCoroutine(PlayAudioSequence());
    }

    IEnumerator PlayAudioSequence()
    {
        foreach (var clip in audioClips)
        {
            audioSource.clip = clip;
            audioSource.Play();

            float clipEndTime = Time.time + clip.length + 2; // Calculate end time of the clip

            while (Time.time < clipEndTime)
            {
                // Check if either hand is close to the stop button
                if (IsHandCloseToButton(leftHand) || IsHandCloseToButton(rightHand))
                {
                    PlayStopClip();
                    yield break; // Exit the coroutine
                }

                yield return null; // Wait for the next frame
            }

            yield return new WaitForSeconds(2); // Additional wait time after each clip
        }
    }

    bool IsHandCloseToButton(GameObject hand)
    {
        return Vector3.Distance(hand.transform.position, stopButton.transform.position) < proximityThreshold;
    }

    void PlayStopClip()
    {
        if (stopClip != null)
        {
            audioSource.Stop(); // Stop the current playing clip
            audioSource.clip = stopClip;
            audioSource.Play();
        }
    }
}

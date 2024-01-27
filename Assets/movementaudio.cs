using UnityEngine;
using System.Collections; // Add this line for IEnumerator

public class PlayAudioPeriodically : MonoBehaviour
{
    public AudioClip audioClip; // Assign the audio clip in the Inspector.
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this object.");
            enabled = false;
        }

        if (audioClip == null)
        {
            Debug.LogError("No AudioClip assigned in the Inspector.");
            enabled = false;
        }

        // Start the coroutine to play the audio periodically.
        StartCoroutine(PlayAudioWithInterval());
    }

    private IEnumerator PlayAudioWithInterval()
    {
        while (true)
        {
            // Play the audio clip.
            audioSource.PlayOneShot(audioClip);

            // Wait for 20 seconds before playing again.
            yield return new WaitForSeconds(34.0f);
        }
    }
}

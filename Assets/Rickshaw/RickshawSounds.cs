using System.Collections;
using UnityEngine;

public class CarSoundManager : MonoBehaviour
{
    // Array to hold different car sound clips
    public AudioClip[] soundClips;

    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Interval for playing sounds (in seconds)
    public float soundPlayInterval = 5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on the GameObject.");
            return;
        }

        if (soundClips.Length == 0)
        {
            Debug.LogWarning("No sound clips assigned to the CarSoundManager.");
            return;
        }

        StartCoroutine(PlayCarSoundsRoutine());
    }

    private IEnumerator PlayCarSoundsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(soundPlayInterval);
            PlayRandomSound();
        }
    }

    private void PlayRandomSound()
    {
        if (audioSource != null && soundClips.Length > 0)
        {
            AudioClip clip = soundClips[Random.Range(0, soundClips.Length)];
            audioSource.clip = clip;
            Debug.Log("Playing sound: " + clip.name); // Debugging line
            audioSource.Play();
        }
    }
}

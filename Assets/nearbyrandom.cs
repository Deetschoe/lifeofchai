using UnityEngine;
using System.Collections;
using System.Collections.Generic; // For using List

public class PlayAudioInRangeRandom : MonoBehaviour
{
    public List<AudioClip> audioClips; // Assign your audio clips in the Inspector.
    private AudioSource audioSource;
    private Coroutine fadeCoroutine;
    private float fadeDuration = 1.0f; // Duration for fade in/out

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1.0f; // Set to 3D audio
        audioSource.volume = 0.0f; // Start with volume at 0
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartFadeAudio(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartFadeAudio(false);
        }
    }

    private void StartFadeAudio(bool fadeIn)
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeAudio(fadeIn));
    }

    private IEnumerator FadeAudio(bool fadeIn)
    {
        // Randomly select an AudioClip
        if (audioClips.Count > 0)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        }
        else
        {
            yield break; // Exit if there are no clips
        }

        float startVolume = fadeIn ? 0.0f : audioSource.volume;
        float endVolume = fadeIn ? 1.0f : 0.0f;
        float elapsedTime = 0.0f;

        if (fadeIn) audioSource.Play();

        while (elapsedTime < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = endVolume;

        if (!fadeIn) audioSource.Stop();
    }
}

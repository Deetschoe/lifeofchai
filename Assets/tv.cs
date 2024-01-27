using System.Collections;
using UnityEngine.Video;
using UnityEngine;

public class TVAudioController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign in Inspector
    private AudioSource audioSource;
    private float maxVolume = 2.0f; // Adjust as needed
    private Coroutine currentFadeCoroutine;

    private void Start()
    {
        audioSource = videoPlayer.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the VideoPlayer GameObject");
            return;
        }
        audioSource.volume = 0.0f;
        videoPlayer.Prepare();
        videoPlayer.loopPointReached += VideoFinished;
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
        if (currentFadeCoroutine != null)
            StopCoroutine(currentFadeCoroutine);

        currentFadeCoroutine = StartCoroutine(FadeAudio(fadeIn));
    }

    private IEnumerator FadeAudio(bool fadeIn)
    {
        float fadeDuration = 2.0f;
        float startVolume = fadeIn ? 0.0f : maxVolume;
        float endVolume = fadeIn ? maxVolume : 0.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = endVolume;
    }

    private void VideoFinished(VideoPlayer vp)
    {
        // Custom logic for when video finishes
    }
}

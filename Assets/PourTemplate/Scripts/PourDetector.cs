using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

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
        print(CalculatePourAngle());

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

        // Play the audio when pouring starts
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
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
}

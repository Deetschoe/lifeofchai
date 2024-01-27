using UnityEngine;

public class XRGrabbableMovementDetector : MonoBehaviour
{
    public AudioSource characterAudioSource; // Reference to the AudioSource component on the character
    public AudioClip movementAudioClip; // Audio clip to play when movement is detected
    public float movementThreshold = 0.1f; // Minimum distance the object must move to trigger the sound

    private Vector3 lastPosition; // Last recorded position of the object

    void Start()
    {
        // Initialize the last position
        lastPosition = transform.position;
    }

    void Update()
    {
        // Check if the object has moved more than the threshold distance
        if (Vector3.Distance(lastPosition, transform.position) > movementThreshold)
        {
            // Play the audio clip if not already playing
            if (!characterAudioSource.isPlaying)
            {
                characterAudioSource.clip = movementAudioClip;
                characterAudioSource.Play();
            }

            // Update the last position
            lastPosition = transform.position;
        }
    }
}

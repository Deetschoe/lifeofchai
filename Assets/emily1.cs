using UnityEngine;

public class emily1 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip moveAndPlayAudioClip;
    public Transform targetLocation;

    private bool heardNoChai = false;
    private bool hasMoved = false;
    private float timer = 0f;

    void Update()
    {
        // Check if the audio has been heard and the object has not yet moved
        if (heardNoChai && !hasMoved)
        {
            timer += Time.deltaTime;

            // When timer reaches 20 seconds, move the object
            if (timer >= 20f)
            {
                MoveToTargetLocation();
                hasMoved = true;
            }
        }
    }

    // Call this method to simulate hearing the "nochai" audio
    public void ListenForNoChai()
    {
        heardNoChai = true;
    }

    private void MoveToTargetLocation()
    {
        // Check if target location is assigned
        if (targetLocation != null)
        {
            // Move Object 2 to the target location
            transform.position = targetLocation.position;
            transform.rotation = targetLocation.rotation;

            // Play audio if available and not already playing
            if (moveAndPlayAudioClip != null && audioSource != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(moveAndPlayAudioClip);
            }
        }
    }

    // Optional: Call this method to set a new target location from other scripts
    public void SetTargetLocation(Transform newTargetLocation)
    {
        targetLocation = newTargetLocation;
    }
}

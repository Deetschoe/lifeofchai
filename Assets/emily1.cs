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
        if (heardNoChai && !hasMoved)
        {
            timer += Time.deltaTime;
            if (timer >= 20f)
            {
                MoveToTargetLocation();
                hasMoved = true;
            }
        }
    }

    public void ListenForNoChai()
    {
        // This function should be called to set heardNoChai to true.
        // Implementation depends on how "nochai" audio is detected.
        heardNoChai = true;
    }

    private void MoveToTargetLocation()
    {
        if (targetLocation != null)
        {
            // Play audio if available
            if (moveAndPlayAudioClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(moveAndPlayAudioClip);
            }

            // Move Object 2 to target location
            transform.position = targetLocation.position;
            transform.rotation = targetLocation.rotation;
        }
    }

    // Optional: Call this method to set a new target location from other scripts
    public void SetTargetLocation(Transform newTargetLocation)
    {
        targetLocation = newTargetLocation;
    }
}

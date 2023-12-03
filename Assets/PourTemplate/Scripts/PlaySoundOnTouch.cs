using UnityEngine;

public class PlaySoundOnProximity : MonoBehaviour
{
    public GameObject fireObject; // Assign the fire object in the inspector
    public float proximityThreshold = 0.15f; // Distance threshold for playing sound
    public AudioSource audioSource;
    private bool isPlaying = false;

    void Update()
    {
        if (fireObject != null)
        {
            // Calculate the distance from the bottom of this object (pot) to the fire object
            float distance = Vector3.Distance(GetBottomPosition(), fireObject.transform.position);

            // Check if the distance is less than the threshold
            if (distance <= proximityThreshold)
            {
                if (!isPlaying)
                {
                    Debug.Log("Pot is close to fire - starting to play sound");
                    audioSource.loop = true;
                    audioSource.Play();
                    isPlaying = true;
                }
            }
            else
            {
                if (isPlaying)
                {
                    Debug.Log("Pot is no longer close to fire - stopping the sound");
                    audioSource.loop = false;
                    audioSource.Stop();
                    isPlaying = false;
                }
            }
        }
    }

    // Helper method to get the bottom position of the pot object
    private Vector3 GetBottomPosition()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            return new Vector3(transform.position.x, collider.bounds.min.y, transform.position.z);
        }
        return transform.position;
    }
}

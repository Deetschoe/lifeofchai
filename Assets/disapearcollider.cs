using UnityEngine;

public class ObjectVisibilityToggle : MonoBehaviour
{
    private bool isPlayerInRoom1 = false;
    private float disappearAfterTime = 80f; // Time in seconds after which the object will disappear
    private bool startTimer = false;
    private float timer = 0f;

    // Public variable to assign the AudioSource component in the inspector
    public AudioSource audioSource; // Assign this in the Unity Inspector with your desired audio source

    void Update()
    {
        // If the timer is started and the player is in room1, start counting
        if (startTimer && isPlayerInRoom1)
        {
            timer += Time.deltaTime;

            if (timer >= disappearAfterTime)
            {
                // Make the object disappear
                gameObject.SetActive(false);

                // Play the audio clip
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                // No need to stop the timer or reset it here since it will be handled in OnTriggerExit
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player enters a collider tagged with "room1"
        if (other.CompareTag("Player") && gameObject.CompareTag("room1"))
        {
            isPlayerInRoom1 = true;
            startTimer = true; // Start the disappear timer
            timer = 0f; // Reset the timer whenever the player enters the collider
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player exits the collider
        if (other.CompareTag("Player") && gameObject.CompareTag("room1"))
        {
            isPlayerInRoom1 = false;
            startTimer = false; // Stop the timer
            timer = 0f; // Reset the timer

            // Make the object reappear as soon as the player leaves
            gameObject.SetActive(true);
        }
    }
}

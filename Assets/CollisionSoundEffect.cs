using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Added for scene management

public class CollisionSoundEffect : MonoBehaviour
{
    public AudioClip soundEffect; // Drag your AudioClip here through the Unity Inspector

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Existing logic to handle sound effects and destroy enemies

            GameObject castle = GameObject.FindGameObjectWithTag("Castle");
            if (castle != null)
            {
                AudioSource castleAudioSource = castle.GetComponent<AudioSource>();
                if (castleAudioSource != null)
                {
                    castleAudioSource.enabled = false;
                }
                else
                {
                    Debug.LogWarning("No AudioSource found on the Castle GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("No GameObject with tag 'Castle' found.");
            }

            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && soundEffect != null)
            {
                audioSource.clip = soundEffect;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource or AudioClip is not assigned on this GameObject.");
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }

            // Start a coroutine to transition to the Waterfront scene after 28 seconds
            StartCoroutine(TransitionToWaterfrontAfterDelay(28f));
        }
    }

    IEnumerator TransitionToWaterfrontAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the "Waterfront" scene
        SceneManager.LoadScene("Waterfront");
    }
}

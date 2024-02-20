using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundEffect : MonoBehaviour
{
    // Drag your AudioClip here through the Unity Inspector
    public AudioClip soundEffect;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Attempt to find the GameObject with the tag "Castle"
            GameObject castle = GameObject.FindGameObjectWithTag("Castle");
            if (castle != null)
            {
                // Try to get the AudioSource component on the "Castle" GameObject
                AudioSource castleAudioSource = castle.GetComponent<AudioSource>();
                if (castleAudioSource != null)
                {
                    // Disable the AudioSource on the "Castle"
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

            // Check if this GameObject's AudioSource and AudioClip are assigned
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null && soundEffect != null)
            {
                // Set the AudioClip to this GameObject's AudioSource
                audioSource.clip = soundEffect;

                // Play the sound effect
                audioSource.Play();
            }
            else
            {
                // Log a warning if AudioSource or AudioClip is not assigned
                Debug.LogWarning("AudioSource or AudioClip is not assigned on this GameObject.");
            }

            // Remove every game object with the tag "Enemy"
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
}

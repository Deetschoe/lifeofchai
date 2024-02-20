using System.Collections;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public AudioSource[] audioSources; // Array to hold the random audio sources
    public AudioSource disappearAudioSource; // AudioSource for the disappear sound
    private bool isEnemyInside = false;

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is tagged as "enemy"
        if (other.CompareTag("Enemy"))
        {
            isEnemyInside = true;
            // Play one of the random audio sources when an enemy enters
            int randomIndex = Random.Range(0, audioSources.Length);
            audioSources[randomIndex].Play();

            // Start the coroutine to make the object disappear after 7 seconds
            StartCoroutine(DisappearAfterTime(7f, other.gameObject));
        }
    }

    private IEnumerator DisappearAfterTime(float time, GameObject enemy)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Check if the enemy is still inside the collider
        if (isEnemyInside && enemy != null)
        {
            // Play the disappear sound
            disappearAudioSource.Play();

            // Wait for the sound to finish before making changes
            yield return new WaitForSeconds(disappearAudioSource.clip.length);

            // Make the object disappear or destroy it
            Destroy(enemy);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the enemy
        if (other.CompareTag("Enemy"))
        {
            isEnemyInside = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PunchDetector : MonoBehaviour
{
    public GameObject holePrefab; // Assign this in the inspector with your hole prefab

    public GameObject leftHandController;
    public GameObject rightHandController;
    public GameObject objectToTeleport; // Object to teleport
    public AudioClip punchSound; // Sound to play on punch

    private AudioSource audioSource; // Audio source to play the sound
    private float punchCooldown = 1.0f;
    private float lastPunchTime = -1.0f;
    private float punchDistanceThreshold = 1f; // Threshold distance to detect a punch

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource if there isn't one already
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Time.time - lastPunchTime < punchCooldown)
        {
            return; // Cooldown not yet elapsed
        }

        // Check if either hand is making a fist and within range of a tamed object
        CheckForPunch(leftHandController);
        CheckForPunch(rightHandController);
    }


private IEnumerator CreateHoleNextToObject(GameObject targetObject)
{
    // Wait for 5 seconds
    yield return new WaitForSeconds(5f);

    // Determine the position for the hole. This example places the hole 1 unit to the right.
    // Adjust the position as needed for your game's logic.
    Vector3 holePosition = targetObject.transform.position + Vector3.right; // Adjust this vector as needed

    // Instantiate the hole prefab at the calculated position
    Instantiate(holePrefab, holePosition, Quaternion.identity);
}

    private void CheckForPunch(GameObject handController)
    {
        // Fist detection logic (e.g., checking for input on the controller)

        // Iterate over all tamed objects to check proximity
        foreach (var tamedObject in GameObject.FindGameObjectsWithTag("tamed"))
        {
            float distance = Vector3.Distance(handController.transform.position, tamedObject.transform.position);
            if (distance <= punchDistanceThreshold)
            {
                // Proximity detected, reduce health
                HealthBarGradient healthBar = tamedObject.GetComponent<HealthBarGradient>();
                if (healthBar != null)
                {
                    healthBar.currentHealth -= 5;
                    lastPunchTime = Time.time;

                    if (healthBar.currentHealth <= 0)
                    {
                        TeleportObjectTo(tamedObject.transform.position);
                        PlayPunchSound();
                        Destroy(tamedObject); // Remove tamed object
                    }
                    break; // Exit the loop if a punch is detected
                }
            }
        }
    }

private void TeleportObjectTo(Vector3 position)
{
    if (objectToTeleport != null)
    {
        objectToTeleport.transform.position = position;
        // Start the coroutine to create a hole next to the object after 5 seconds
        StartCoroutine(CreateHoleNextToObject(objectToTeleport));
    }
}


    private void PlayPunchSound()
    {
        if (punchSound != null)
        {
            audioSource.PlayOneShot(punchSound);
        }
    }
}

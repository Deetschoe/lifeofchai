using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PunchDetector : MonoBehaviour
{
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

using System.Collections;
using UnityEngine;

public class AudioAndMove : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip noChaiSound; // Audio clip to play when triggered
    public float moveSpeed = 2.0f; // Speed of movement

    private bool isMoving = false; // Flag to track if the GameObject is currently moving

    // Target position coordinates
    public float targetX = 0f; // X coordinate of the target position
    public float targetY = 0f; // Y coordinate of the target position
    public float targetZ = 0f; // Z coordinate of the target position

    // Method to trigger audio playback and movement
    public void PlayAudioAndMove()
    {
        if (!isMoving)
        {
            // Check the liquid level of the cup
            float liquidLevel = GetLiquidLevel();

            if (liquidLevel == 0)
            {
                StartCoroutine(PlayAudioAndMoveCoroutine());
            }
        }
    }

    // Coroutine to play audio and move the GameObject
    private IEnumerator PlayAudioAndMoveCoroutine()
    {
        // Play the audio clip
        if (audioSource != null && noChaiSound != null)
        {
            audioSource.clip = noChaiSound;
            audioSource.Play();

            // Log a message when the audio source starts playing
            Debug.Log("Audio Source is playing.");
        }

        // Move the GameObject towards the target position
        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(targetX, targetY, targetZ);

        while (transform.position != endPosition)
        {
            float journeyLength = Vector3.Distance(startPosition, endPosition);
            float journeyTime = journeyLength / moveSpeed;
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

            yield return null;
        }

        // Ensure the GameObject reaches the exact target position
        transform.position = endPosition;
        isMoving = false;
    }

    // Simulate the method to get the liquid level from the cup (replace this with your actual logic)
    private float GetLiquidLevel()
    {
        // Replace this with your logic to obtain the actual liquid level
        // For demonstration purposes, return 0 for now
        return 0;
    }
}

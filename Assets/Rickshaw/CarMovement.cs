using System.Collections;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 12f;
    public float slowDownSpeed = 1f; // Speed of the car when slowing down for an NPC
    public float detectionDistance = 1f; // Distance to detect NPCs in front of the car
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Bounds activeArea;  // Define an active area for the car
    public float bumpiness = 0.5f;
    public AudioClip[] carSounds; // Array to hold different car sound clips

    private AudioSource audioSource; // Reference to the AudioSource component
    private bool movingToEnd = true;
    private float originalYPosition;
    private bool isCurrentlyActive = true;
    private bool hasLeftActiveArea = false; // Tracks if the car has left the active area
    private bool isMoving = false; // Tracks if the car is currently moving

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the activeArea's center with its size
        Gizmos.color = new Color(0, 1, 1, 0.5f);
        Gizmos.DrawCube(activeArea.center, activeArea.size);
    }

    private void Start()
    {
        originalYPosition = transform.position.y;
        StartCoroutine(ReactivateCarRoutine());
        // Create AudioSource dynamically if it's not already attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (!activeArea.Contains(transform.position))
        {
            if (isCurrentlyActive)
            {
                Debug.Log("Car has left the active area");
                isCurrentlyActive = false;
                hasLeftActiveArea = true;
                //FadeOutAndDeactivate();
                StopCarSound();

                // Start the respawn routine
                StartCoroutine(ReactivateCarRoutine());
            }
            return;
        }

        if (!isCurrentlyActive)
        {
            Debug.Log("Car is back in the active area");
            isCurrentlyActive = true;
        }

        // Check for NPCs in front of the car and adjust speed
        RaycastHit hit;
        Vector3 direction = movingToEnd ? (endPoint - transform.position).normalized : (startPoint - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out hit, detectionDistance))
        {
            if (hit.collider.CompareTag("npc"))
            {
                // Slow down when approaching an NPC
                speed = slowDownSpeed;
            }
            else
            {
                // Resume normal speed
                speed = 12f;
            }
        }
        else
        {
            // Resume normal speed
            speed = 12f;
        }

        // Move the car
        Vector3 targetPosition = movingToEnd ? endPoint : startPoint;
        Vector3 previousPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Add bumpiness
        float newYPosition = originalYPosition + Mathf.Sin(Time.time * speed) * bumpiness;
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

        // Check if the car is moving
        if (transform.position != previousPosition && !isMoving)
        {
            isMoving = true;
            PlayRandomCarSound();
        }
        else if (transform.position == previousPosition && isMoving)
        {
            isMoving = false;
            StopCarSound();
        }

        // Check if reached the target position
        if (transform.position == targetPosition)
        {
            movingToEnd = !movingToEnd;
        }
    }

    private void FadeOutAndDeactivate()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator ReactivateCarRoutine()
    {
        if (hasLeftActiveArea)
        {
            yield return new WaitForSeconds(Random.Range(1f, 120f));
            Debug.Log("Reactivating the car");
            gameObject.SetActive(true);
            transform.position = startPoint;
            movingToEnd = true;
            isCurrentlyActive = true;
            isMoving = false;
            hasLeftActiveArea = false;
        }
    }

    private void PlayRandomCarSound()
    {
        if (audioSource != null && carSounds.Length > 0)
        {
            AudioClip clip = carSounds[Random.Range(0, carSounds.Length)];
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void StopCarSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

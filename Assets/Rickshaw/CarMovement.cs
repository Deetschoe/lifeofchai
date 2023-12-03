using System.Collections;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 startPoint;
    public Vector3 endPoint;
    public Bounds activeArea;  // Define an active area for the car
    public float bumpiness = 0.5f;
    public AudioClip[] carSounds; // Array to hold different car sound clips

    private AudioSource audioSource; // Reference to the AudioSource component
    private bool movingToEnd = true;
    private float originalYPosition;
    private bool isCurrentlyActive = true;
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
                FadeOutAndDeactivate();
                StopCarSound();
            }
            return;
        }
        else if (!isCurrentlyActive)
        {
            Debug.Log("Car is back in the active area");
            isCurrentlyActive = true;
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
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(30f, 60f));
            if (!gameObject.activeSelf)
            {
                Debug.Log("Reactivating the car");
                gameObject.SetActive(true);
                transform.position = startPoint;
                movingToEnd = true;
                isCurrentlyActive = true;
                isMoving = false;
            }
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


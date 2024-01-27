using UnityEngine;

public class MoveAndPlaySound : MonoBehaviour
{
    public float moveDistance = 5.0f; // Distance in units to move backwards
    public float speed = 0.1f; // Speed at which the object should move
    private float movedDistance = 1.5f; // Distance that the object has already moved
    public AudioClip soundToPlay;
    private AudioSource audioSource;
    private bool hasPlayedSound = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Move the object as long as it hasn't reached the desired distance
        if (movedDistance < moveDistance)
        {
            float step = speed * Time.deltaTime; // Calculate distance to move this frame
            transform.Translate(Vector3.forward * step); // Move forwards
            movedDistance += step;
        }
        else if (!hasPlayedSound)
        {
            // Play the sound once after reaching the distance
            PlaySound();
            hasPlayedSound = true; // Ensure the sound is played only once
        }
    }


    void PlaySound()
    {
        if (soundToPlay != null && audioSource != null)
        {
            audioSource.clip = soundToPlay;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is missing");
        }
    }
}

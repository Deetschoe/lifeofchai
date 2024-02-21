using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public AudioClip[] swingSounds; // Assign these in the Inspector
    public AudioClip[] contactSounds; // Assign these in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Ensure there's an AudioSource component and configure it
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // Add AudioSource if not already attached
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Example of playing a swing sound when swinging the sword
        // This should be replaced with your actual swing detection logic
        if (Input.GetKeyDown(KeyCode.Space)) // Example trigger, replace with your swing detection
        {
            PlaySwingSound();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the sword comes into contact with an object tagged as "Enemy"
        if (other.CompareTag("Enemy"))
        {
            PlayContactSound();
        }
    }

    void PlaySwingSound()
    {
        if (swingSounds.Length > 0)
        {
            AudioClip swingSound = swingSounds[Random.Range(0, swingSounds.Length)];
            audioSource.PlayOneShot(swingSound);
        }
    }

    void PlayContactSound()
    {
        if (contactSounds.Length > 0)
        {
            AudioClip contactSound = contactSounds[Random.Range(0, contactSounds.Length)];
            audioSource.PlayOneShot(contactSound);
        }
    }
}

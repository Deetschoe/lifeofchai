using UnityEngine;
using System.Collections.Generic;

public class PotSounds : MonoBehaviour
{
    public List<AudioClip> waterSounds = new List<AudioClip>();
    public List<AudioClip> metalSounds = new List<AudioClip>();
    private AudioSource audioSource;
    private Vector3 lastPosition;
    public float movementThreshold = 0.1f; // Adjust this value as needed
    public float collisionThreshold = 2.0f; // Adjust this value to set the impact strength required for metal sound

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (IsShaking())
        {
            // Play a random water sound
            if (!audioSource.isPlaying || !waterSounds.Contains(audioSource.clip))
            {
                audioSource.clip = waterSounds[Random.Range(0, waterSounds.Count)];
                audioSource.Play();
            }
        }
        lastPosition = transform.position;
    }

    bool IsShaking()
    {
        float movement = (transform.position - lastPosition).magnitude;
        return movement > movementThreshold;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Contact") && collision.relativeVelocity.magnitude > collisionThreshold)
        {
            // Play a random metal sound
            audioSource.clip = metalSounds[Random.Range(0, metalSounds.Count)];
            audioSource.Play();
        }
    }
}

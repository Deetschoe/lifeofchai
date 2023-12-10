using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emilyApproaches : MonoBehaviour
{
    public Transform targetObject; // Target object to move towards
    public AudioSource audioSource; // Audio source to play
    public float speed = 5f; // Speed of movement

    private bool isAudioPlayed = false;

    void Update()
    {
        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move
        
        
        // Play audio if not already played
        if (!isAudioPlayed)
        {
            audioSource.Play();
            isAudioPlayed = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetObject.position, step);

    }
}

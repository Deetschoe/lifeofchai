using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcinteraction : MonoBehaviour
{
    public AudioSource initialVoiceClip; // First audio clip
    public AudioSource secondVoiceClip; // Second audio clip
    public float moveSpeed = 0.5f; // Speed of movement

    private float startX = 10f; // Starting position on the X-axis
    private float targetX = 1f; // Target position on the X-axis
    private float startZ; // Starting position on the Z-axis
    private float targetZ = 10f; // Target position on the Z-axis
    private float currentLerpTimeX = 0f; // Current interpolation time for X-axis movement
    private float currentLerpTimeZ = 0f; // Current interpolation time for Z-axis movement
    private bool hasPlayedInitialAudio = false; // To ensure the first audio is played only once
    private bool hasPlayedSecondAudio = false; // To ensure the second audio is played only once
    private bool isMovingToTargetX = true; // Initially moving in the X direction
    private bool isMovingToTargetZ = false; // Initially not moving in the Z direction
    private GameObject cup; // To keep track of the cup GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the starting position
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement in the X direction
        if (isMovingToTargetX)
        {
            MoveInXDirection();
        }

        // Check for the cup and handle the second audio and Z movement
        HandleCupInteraction();
    }

    // Method to handle movement in the X direction
    void MoveInXDirection()
    {
        currentLerpTimeX += Time.deltaTime * moveSpeed;
        if (currentLerpTimeX > 1f)
        {
            currentLerpTimeX = 1f;
            isMovingToTargetX = false;
            PlayInitialAudioOnce();
        }

        float newX = Mathf.Lerp(startX, targetX, currentLerpTimeX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    // Play the initial audio clip once
    void PlayInitialAudioOnce()
    {
        if (!hasPlayedInitialAudio)
        {
            initialVoiceClip.Play();
            hasPlayedInitialAudio = true;
        }
    }

    // Handle the interaction with the cup
    void HandleCupInteraction()
    {
        if (!hasPlayedSecondAudio && IsCupOnTop())
        {
            AttachCup();
            PlaySecondAudio();
        }

        // After playing second audio, start moving on the Z-axis
        if (!secondVoiceClip.isPlaying && hasPlayedSecondAudio && !isMovingToTargetZ)
        {
            isMovingToTargetZ = true;
            StartCoroutine(MoveToTargetZ());
        }
    }

    // Attach the cup to the character
    void AttachCup()
    {
        // Disable the rigidbody and collider if necessary
        Rigidbody cupRigidbody = cup.GetComponent<Rigidbody>();
        if (cupRigidbody != null)
        {
            cupRigidbody.isKinematic = true; // Make rigidbody not be affected by physics
        }

        Collider cupCollider = cup.GetComponent<Collider>();
        if (cupCollider != null)
        {
            cupCollider.enabled = false; // Disable the collider
        }

        // Make the cup a child of this object
        cup.transform.SetParent(transform);

        // Adjust the local position and rotation of the cup as needed
        cup.transform.localPosition = new Vector3(0, 1, 0); // Adjust this as needed
        cup.transform.localRotation = Quaternion.identity;
    }

    // Play the second audio clip
    void PlaySecondAudio()
    {
        secondVoiceClip.Play();
        hasPlayedSecondAudio = true;
    }

    // Coroutine to move the character on the Z-axis
    IEnumerator MoveToTargetZ()
    {
        while (currentLerpTimeZ < 1f)
        {
            currentLerpTimeZ += Time.deltaTime * moveSpeed;
            float newZ = Mathf.Lerp(startZ, targetZ, currentLerpTimeZ);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
            yield return null;
        }
    }

    // Check if there is a cup on top and keep a reference to it
    bool IsCupOnTop()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("cup"))
            {
                cup = hitCollider.gameObject; // Keep a reference to the cup
                return true;
            }
        }
        return false;
    }
}

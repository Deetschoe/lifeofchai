using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class npcinteraction : MonoBehaviour
{
    public AudioSource audioSource; // Single AudioSource for playing clips
    public AudioClip noChaiSound; // Array of initial voice clips

    public AudioClip[] initialVoiceClips; // Array of initial voice clips
    public AudioClip[] secondVoiceClips; // Array of second voice clips
    public float moveSpeed = 0.5f; // Speed of movement

    private int currentClipIndex = 0; // Index for the current set of clips
    private float startX = 10f; // Starting position on the X-axis
    private float targetX = 1f; // Target position on the X-axis
    private float startZ; // Starting position on the Z-axis
    private float targetZ = 10f; // Target position on the Z-axis
    private float currentLerpTimeX = 0f; // Current interpolation time for X-axis movement
    private float currentLerpTimeZ = 0f; // Current interpolation time for Z-axis movement
    private bool hasPlayedInitialAudio = false; // To ensure the first audio is played only once
    private bool hasPlayedSecondAudio = false; // To ensure the second audio is played only once

    private bool hasPlayedEndAudio = false; // To ensure the second audio is played only once


    private bool isMovingToTargetX = true; // Initially moving in the X direction
    private bool isMovingToTargetZ = false; // Initially not moving in the Z direction
    private GameObject cup; // To keep track of the cup GameObject


    public GameObject foig; // To keep track of the cup GameObject

    public GameObject emily;


    private Vector3 originalPosition; // Original position of the NPC

    // Event to notify when NPC interaction is complete
    public delegate void InteractionComplete();
    public event InteractionComplete OnInteractionComplete;

    // Start is called before the first frame update
    void Start()
    {
        // Store the original position
        originalPosition = transform.position;

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
        if (!hasPlayedInitialAudio && initialVoiceClips.Length > currentClipIndex)
        {
            audioSource.clip = initialVoiceClips[currentClipIndex];
            audioSource.Play();
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
        if (!audioSource.isPlaying && hasPlayedSecondAudio && !isMovingToTargetZ)
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
        cup.transform.localRotation = Quaternion.identity;
    }

    // Play the second audio clip
    void PlaySecondAudio()
    {
        if (secondVoiceClips.Length > currentClipIndex)
        {
            audioSource.clip = secondVoiceClips[currentClipIndex];
            audioSource.Play();
            hasPlayedSecondAudio = true;
        }
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

        // Reset the NPC to its original position and state
        ResetNPC();

        // Trigger the interaction complete event
        OnInteractionComplete?.Invoke();
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

                // Get the LiquidControl script attached to the cup
                LiquidControl liquidControl = cup.GetComponent<LiquidControl>();

                if (liquidControl != null)
                {
                    // Access and print the liquidLevel
                    print("Liquid Level: " + liquidControl.liquidLevel);
                }
                else
                {
                    print("LiquidControl script not found on the cup GameObject");
                }


                if (liquidControl.liquidLevel == 0)
                {
                    if (hasPlayedEndAudio == false)
                    {
                        hasPlayedEndAudio = true;

                        audioSource.clip = noChaiSound;
                        audioSource.Play();




                        // Try to get the script with the specified name
                        EnvironmentAndParticleSystemController script = foig.GetComponent<EnvironmentAndParticleSystemController>();

                        if (script != null)
                        {
                            // Enable the script or perform any other actions
                            script.enabled = true;
                        }

                        StartCoroutine(EnableScriptAfterDelay());
                    }



                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }


    IEnumerator EnableScriptAfterDelay()
    {
        // Wait for the initial 20 seconds
        yield return new WaitForSeconds(20);

        // Now that 20 seconds have passed, try to get and enable the script
        emilyApproaches script2 = emily.GetComponent<emilyApproaches>();

        if (script2 != null)
        {
            script2.enabled = true;
        }

        // Wait for an additional 10 seconds
        yield return new WaitForSeconds(16);

        // Now load the new scene
        SceneManager.LoadScene("Rickshaw");
    }


    // Reset the NPC for the next interaction
    private void ResetNPC()
    {
        // Destroy the cup
        if (cup != null)
        {
            Destroy(cup);
        }

        // Reset the NPC position and state
        transform.position = originalPosition;
        transform.rotation = transform.rotation = Quaternion.identity;

        currentLerpTimeX = 0f;
        currentLerpTimeZ = 0f;
        hasPlayedInitialAudio = false;
        hasPlayedSecondAudio = false;
        isMovingToTargetX = true;
        isMovingToTargetZ = false;

        // Increment the clip index for the next interaction
        currentClipIndex = (currentClipIndex + 1) % initialVoiceClips.Length;
    }
}
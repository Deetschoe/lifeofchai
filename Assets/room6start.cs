using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    public AudioClip clip; // Assign this in the Inspector
    private AudioSource audioSource;
    public RuntimeAnimatorController animatorController; // To assign an Animator Controller
    private Animator animator; // The Animator component

    void Start()
    {
        // Attempt to get the Animator component on the GameObject
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            // If no Animator component is found, add one and assign the controller
            animator = gameObject.AddComponent<Animator>();
        }

        // Assign the Animator Controller if it's provided
        if (animatorController != null)
        {
            animator.runtimeAnimatorController = animatorController;
        }
        else
        {
            Debug.LogError("Animator Controller not assigned. Please assign one in the inspector or through script.");
        }

        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing from this GameObject. Attach one.");
        }

        // Initially set "inRoom6" to false
        if (animator != null)
        {
            animator.SetBool("inRoom6", false); // Ensure this is the correct parameter name in your Animator
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is tagged as Player
        if (other.CompareTag("Player"))
        {
            // Play the audio if the clip is assigned and the AudioSource exists
            if (clip != null && audioSource != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }

            // Set "inRoom6" to true to trigger the animation
            if (animator != null)
            {
                animator.SetBool("inRoom6", true); // This will queue the animation based on your Animator's setup
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider is tagged as Player
        if (other.CompareTag("Player"))
        {
            // Stop the audio
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // Use Stop() to completely stop the audio
            }

            // Optionally reset "inRoom6" to false if you want to reset the animation state
            if (animator != null)
            {
                animator.SetBool("inRoom6", false); // This will reset the animation based on your Animator's setup
            }
        }
    }
}

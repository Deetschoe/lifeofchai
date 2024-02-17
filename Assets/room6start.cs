using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    public AudioClip clip;         // Assign this in the Inspector
    private AudioSource audioSource;
    public RuntimeAnimatorController animatorController; // To assign an Animator Controller
    private Animator animator;     // The Animator component
    public string animationName = "YourAnimationName"; // Set the name of your animation

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

            // Play the animation if the animator and animation name are properly assigned
            if (animator != null && animator.runtimeAnimatorController != null && !string.IsNullOrEmpty(animationName))
            {
                animator.SetBool(animationName, true); // Assuming it's a boolean trigger in your Animator
                // Use animator.Play(animationName); if you want to play it directly without using parameters
            }
        }
    }
}

using UnityEngine;

public class ToggleAnimatorOnPlayerEnter : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
        // Initially disable the Animator
        if (animator != null) animator.enabled = false;
        else Debug.LogError("ToggleAnimatorOnPlayerEnter: No Animator component found on this GameObject.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering GameObject is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Enable the Animator component
            if (animator != null) animator.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting GameObject is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Disable the Animator component
            if (animator != null) animator.enabled = false;
        }
    }
}

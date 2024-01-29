using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public AudioClip attackSound; // Assign this in the Unity Inspector

    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private void Start()
    {
        // Get the Animator and AudioSource components
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Ensure there is an AudioSource component attached to this GameObject
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the character enters a trigger collider with the "untamed" tag
        if (other.CompareTag("untamed"))
        {
            animator.SetBool(IsAttacking, true);
            animator.SetBool(IsRunning, false);
            PlayAttackSound();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the character exits a trigger collider with the "untamed" tag
        if (other.CompareTag("untamed"))
        {
            animator.SetBool(IsAttacking, false);
            animator.SetBool(IsRunning, true);
        }
    }

    private void PlayAttackSound()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }
}

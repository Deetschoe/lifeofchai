using UnityEngine;

public class TriggerInteraction : MonoBehaviour
{
    public AudioClip clip; // Assign this in the Inspector
    private AudioSource audioSource;
    public RuntimeAnimatorController animatorController; // To assign an Animator Controller
    private Animator animator; // The Animator component

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = gameObject.AddComponent<Animator>();
        }

        if (animatorController != null)
        {
            animator.runtimeAnimatorController = animatorController;
        }
        else
        {
            Debug.LogError("Animator Controller not assigned. Please assign one in the inspector or through script.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not present
            Debug.LogWarning("AudioSource component was missing and has been added.");
        }

        // Ensure this parameter name matches your Animator's setup
        if (animator != null)
        {
            animator.SetBool("inRoom6", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered; setting inRoom6 to true");

            if (clip != null && audioSource != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }

            if (animator != null)
            {
                animator.SetBool("inRoom6", true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Player exited; setting inRoom6 to false");

        if (other.CompareTag("Player"))
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            if (animator != null)
            {
                animator.SetBool("inRoom6", false);
            }
        }
    }
}

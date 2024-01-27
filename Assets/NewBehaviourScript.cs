using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Example condition to start the animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimation("YourAnimationName");
        }
    }

    void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }
}

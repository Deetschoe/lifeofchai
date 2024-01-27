using UnityEngine;

public class UntamedCreatureBehavior : MonoBehaviour
{
    // Existing fields...
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing!");
            return;
        }

        SetIdleAnimation();
    }

    void Update()
    {
        // Check for nearby tamed creatures and engage in interaction
        // Example interaction logic
    }

    public void EngageCombat(GameObject tamedCreature)
    {
        SetAngryAnimation();
        // Implement combat logic here...
    }

    void SetIdleAnimation()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isAngry", false);
        animator.SetBool("isAttacking", false);
    }

    void SetAngryAnimation()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isAngry", true);
    }

    public void SetAttackingAnimation()
    {
        animator.SetBool("isAngry", false);
        animator.SetBool("isAttacking", true);
    }

    // Additional methods...
}

using UnityEngine;
using System.Collections;

public class CreatureBehavior : MonoBehaviour
{
    public float interactionDistance = 5f; // Max distance to search for untamed creatures
    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if (animator == null)
        {
            Debug.LogError("Animator component missing!");
            return;
        }

        SetIdleAnimation();
    }

    public void StartInteraction()
    {
        GameObject nearestUntamedCreature = FindNearestUntamedCreature();
        if (nearestUntamedCreature != null)
        {
            SetAttackingAnimation();
            // Implement additional logic for interaction here
        }
    }

    GameObject FindNearestUntamedCreature()
    {
        GameObject[] untamedCreatures = GameObject.FindGameObjectsWithTag("Untamed");
        GameObject nearestCreature = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject creature in untamedCreatures)
        {
            float distance = Vector3.Distance(transform.position, creature.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCreature = creature;
            }
        }

        return nearestCreature;
    }

    void SetIdleAnimation()
    {
        animator.SetBool("isIdle", true);
        animator.SetBool("isAttacking", false);
    }

    void SetAttackingAnimation()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isAttacking", true);
        // Add logic for moving towards and attacking the untamed creature
    }

    // Add additional methods as needed for movement and interaction
}

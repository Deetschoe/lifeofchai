using UnityEngine;

public class NPCGuide : MonoBehaviour
{
    private GameObject player; // Changed from public Transform player
    private float followDistance = 2f; // The distance at which NPC starts following the player
    private Animator animator; // Animator reference
    private float moveSpeed = 2f; // NPC move speed

    void Start()
    {
        // Find the player GameObject by tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Calculate distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the NPC should start following the player
        if (distanceToPlayer > followDistance)
        {
            // Move towards the player
            MoveTowardsPlayer();
        }
        else
        {
            // Stop moving and update animation
            animator.SetBool("IsWalking", false);
        }
    }

    void MoveTowardsPlayer()
    {
        // Move towards the player at moveSpeed
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        // Always look at the player (optional)
        transform.LookAt(player.transform.position);

        // Check if the NPC has started moving towards the player
        animator.SetBool("IsWalking", true);
    }
}

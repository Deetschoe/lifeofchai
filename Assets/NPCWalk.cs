using UnityEngine;
using System.Collections.Generic;
public class NPCGuide : MonoBehaviour
{
    public Transform player;
    public List<Transform> checkpoints;
    private int currentCheckpointIndex = 0;
    private float proximityThreshold = 2f;
    private Animator animator; // Animator reference
    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (currentCheckpointIndex < checkpoints.Count)
        {
            bool isMoving = MoveToCurrentCheckpoint();
            CheckPlayerProximity();
            // Update the animator's IsWalking parameter
            animator.SetBool("IsWalking", isMoving);
        }
    }
    bool MoveToCurrentCheckpoint()
    {
        Transform currentCheckpoint = checkpoints[currentCheckpointIndex];
        Vector3 startPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, currentCheckpoint.position, Time.deltaTime);
        // Check if the position has changed to determine if the character is moving
        return startPosition != transform.position;
    }
    void CheckPlayerProximity()
    {
        if (Vector3.Distance(player.position, checkpoints[currentCheckpointIndex].position) < proximityThreshold)
        {
            currentCheckpointIndex++; // Move to the next checkpoint
        }
    }
}
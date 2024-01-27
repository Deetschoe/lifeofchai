using UnityEngine;
using System.Collections.Generic;

public class NPCWalk : MonoBehaviour
{
    public Transform player;
    public List<Transform> checkpoints;
    private int currentCheckpointIndex = 0;
    private float proximityThreshold = 1.5f;

    void Update()
    {
        if (currentCheckpointIndex < checkpoints.Count)
        {
            MoveToCurrentCheckpoint();
            CheckPlayerProximity();
        }
    }

    void MoveToCurrentCheckpoint()
    {
        // Add movement logic here. For example, simple direct movement:
        Transform currentCheckpoint = checkpoints[currentCheckpointIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentCheckpoint.position, Time.deltaTime);
        // You can replace this with more sophisticated pathfinding logic if necessary.
    }

    void CheckPlayerProximity()
    {
        if (Vector3.Distance(player.position, checkpoints[currentCheckpointIndex].position) < proximityThreshold)
        {
            currentCheckpointIndex++; // Move to the next checkpoint
        }
    }
}

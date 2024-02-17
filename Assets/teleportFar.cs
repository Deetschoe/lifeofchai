using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWhenClose : MonoBehaviour
{
    public Transform planeTransform; // Assign this in the inspector with your plane object
    public Vector2 teleportLocation; // Assign the x, y values for teleport location in the inspector
    public float proximityThreshold = 1.0f; // How close the player needs to be to the plane to teleport

    void Update()
    {
        CheckProximityAndTeleport();
    }

    private void CheckProximityAndTeleport()
    {
        // Calculate the distance between the player and the plane
        float distance = Vector3.Distance(transform.position, planeTransform.position);
        
        // If within the specified proximity threshold, teleport the player
        if (distance <= proximityThreshold)
        {
            // Assuming you only want to change the x and y, and keep the player's current z position
            transform.position = new Vector3(teleportLocation.x, teleportLocation.y, transform.position.z);
        }
    }
}
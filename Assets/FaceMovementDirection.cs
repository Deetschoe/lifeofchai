using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceNearestUntamed : MonoBehaviour
{

    void Update()
    {
        // Find all game objects with the tag 'Untamed'
        GameObject[] untamedObjects = GameObject.FindGameObjectsWithTag("untamed");

        // Check if there are any untamed objects in the scene
        if (untamedObjects.Length > 0)
        {
            GameObject nearestObject = null;
            float nearestDistance = Mathf.Infinity;

            // Find the nearest untamed object
            foreach (GameObject obj in untamedObjects)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestObject = obj;
                }
            }

            // If a nearest object is found, rotate towards it
            if (nearestObject != null)
            {
                // Calculate target direction but ignore Y axis
                Vector3 targetDirection = nearestObject.transform.position - transform.position;
                targetDirection.y = 0; // This ensures rotation only in X and Z directions

                // Calculate the target rotation based on the target direction
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

                // Rotate the object towards the nearest object
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f); // Adjust 5.0f for smoother or faster rotation
            }
        }
    }
}

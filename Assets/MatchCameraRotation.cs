using UnityEngine;

public class MatchCameraPositionAndRotation : MonoBehaviour
{
    public Transform vrCamera;
    public float heightOffset = -0.5f; // Adjust this value to match the hip level

    void Update()
    {
        if (vrCamera != null)
        {
            // Calculate the new position: Follow the camera's X and Z, but with an offset in Y
            Vector3 newPosition = vrCamera.position;
            newPosition.y += heightOffset;

            transform.position = newPosition;

            // Set the GameObject's Y rotation to match the camera's Y rotation
            Vector3 currentRotation = transform.eulerAngles;
            Vector3 cameraRotation = vrCamera.eulerAngles;

            transform.eulerAngles = new Vector3(currentRotation.x, cameraRotation.y, currentRotation.z);
        }
    }
}

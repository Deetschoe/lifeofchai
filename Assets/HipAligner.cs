using UnityEngine;

public class HipAligner : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 hipOffset = new Vector3(-0.3f, -0.9f, 0); // Adjust this offset as needed

    void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned.");
            return;
        }

        // Get the camera's position and y-axis rotation
        Vector3 cameraPosition = mainCamera.transform.position;
        Quaternion cameraYRotation = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0);

        // Calculate the approximate hip position
        Vector3 hipPosition = cameraPosition + cameraYRotation * hipOffset;

        // Set the sphere's position
        transform.position = hipPosition;
    }
}

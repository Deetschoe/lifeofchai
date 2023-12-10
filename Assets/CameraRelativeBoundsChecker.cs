using UnityEngine;

public class CameraRelativeBoundsChecker : MonoBehaviour
{
    public Transform parentObject; // Parent GameObject
    public Vector3 relativeMinBounds; // Relative to the parent object
    public Vector3 relativeMaxBounds;

    private Camera cameraComponent; // Camera component
    private Vector3 actualMinBounds;
    private Vector3 actualMaxBounds;

    void Start()
    {
        UpdateBounds();
        cameraComponent = GetComponent<Camera>(); // Get the camera component
    }

    void Update()
    {
        CheckBounds();
    }

    void CheckBounds()
    {
        if (transform.position.x < actualMinBounds.x || transform.position.x > actualMaxBounds.x ||
            transform.position.y < actualMinBounds.y || transform.position.y > actualMaxBounds.y ||
            transform.position.z < actualMinBounds.z || transform.position.z > actualMaxBounds.z)
        {
            if (cameraComponent.enabled)
            {
                cameraComponent.enabled = false; // Disable the camera if it's out of bounds
            }
        }
        else
        {
            if (!cameraComponent.enabled)
            {
                cameraComponent.enabled = true; // Enable the camera if it's within bounds
            }
        }
    }

    void OnDrawGizmos()
    {
        if (parentObject == null)
            return;

        UpdateBounds();

        // Set the color of the Gizmo
        Gizmos.color = Color.green;

        // Calculate the center and size of the Gizmo box
        Vector3 center = (actualMinBounds + actualMaxBounds) / 2;
        Vector3 size = actualMaxBounds - actualMinBounds;

        // Draw the wireframe box
        Gizmos.DrawWireCube(center, size);
    }

    void UpdateBounds()
    {
        actualMinBounds = parentObject.position + Vector3.Scale(relativeMinBounds, parentObject.localScale);
        actualMaxBounds = parentObject.position + Vector3.Scale(relativeMaxBounds, parentObject.localScale);
    }
}

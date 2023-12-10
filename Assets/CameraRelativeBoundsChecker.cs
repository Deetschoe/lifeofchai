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
        cameraComponent = GetComponent<Camera>(); // Get the camera component
        UpdateBounds();

        // Initialize camera position at the center of the bounds
        Vector3 centerPosition = (actualMinBounds + actualMaxBounds) / 2;
        transform.position = centerPosition;
    }

    void Update()
    {
        CheckBounds();
    }

    void CheckBounds()
    {
        // Check if the camera is out of bounds
        if (!IsWithinBounds(transform.position))
        {
            cameraComponent.enabled = false; // Disable the camera
        }
        else
        {
            cameraComponent.enabled = true; // Enable the camera
        }
    }

    bool IsWithinBounds(Vector3 position)
    {
        // Convert the position to the local space of the parent
        Vector3 localPos = parentObject.InverseTransformPoint(position);
        return localPos.x >= relativeMinBounds.x && localPos.x <= relativeMaxBounds.x &&
               localPos.y >= relativeMinBounds.y && localPos.y <= relativeMaxBounds.y &&
               localPos.z >= relativeMinBounds.z && localPos.z <= relativeMaxBounds.z;
    }

    void OnDrawGizmos()
    {
        if (parentObject == null)
            return;

        UpdateBounds();

        // Set the color of the Gizmo
        Gizmos.color = Color.green;

        // Draw the wireframe box
        Gizmos.matrix = Matrix4x4.TRS(parentObject.position, parentObject.rotation, parentObject.localScale);
        Gizmos.DrawWireCube((relativeMinBounds + relativeMaxBounds) / 2, relativeMaxBounds - relativeMinBounds);
    }

    void UpdateBounds()
    {
        // Transform the bounds from local space of the parent to world space
        actualMinBounds = parentObject.TransformPoint(relativeMinBounds);
        actualMaxBounds = parentObject.TransformPoint(relativeMaxBounds);
    }
}

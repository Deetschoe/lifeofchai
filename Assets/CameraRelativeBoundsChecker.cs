using UnityEngine;

public class CameraRelativeBoundsChecker : MonoBehaviour
{
    public Transform parentObject; // Parent GameObject
    public Vector3 relativeMinBounds; // Relative to the parent object
    public Vector3 relativeMaxBounds;
    public float fadeSpeed = 1f; // Speed of the fade effect

    private Camera cameraComponent; // Camera component
    private Vector3 actualMinBounds;
    private Vector3 actualMaxBounds;
    private Color originalClearColor;
    private Color targetColor = Color.black; // Target color when out of bounds

    void Start()
    {
        cameraComponent = GetComponent<Camera>(); // Get the camera component
        UpdateBounds();
        originalClearColor = cameraComponent.backgroundColor; // Store the original clear color

        // Initialize camera position at the center of the bounds
        Vector3 centerPosition = (actualMinBounds + actualMaxBounds) / 2;
        transform.position = centerPosition;
    }

    void Update()
    {
        UpdateCameraFade();
    }

    void UpdateCameraFade()
    {
        bool isWithinBounds = IsWithinBounds(transform.position);
        Color targetClearColor = isWithinBounds ? originalClearColor : targetColor;
        cameraComponent.backgroundColor = Color.Lerp(cameraComponent.backgroundColor, targetClearColor, fadeSpeed * Time.deltaTime);
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
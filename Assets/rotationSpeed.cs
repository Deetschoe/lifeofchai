using UnityEngine;

public class LightOrbit : MonoBehaviour
{
    public float rotationSpeed = 10.0f; // Adjust this value to control the rotation speed.

    private void Update()
    {
        // Calculate the axis for rotation (upwards in this case).
        Vector3 axis = Vector3.up;

        // Rotate the light around the world origin.
        transform.RotateAround(Vector3.zero, axis, rotationSpeed * Time.deltaTime);
    }
}

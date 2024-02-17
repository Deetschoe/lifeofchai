using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCylinder : MonoBehaviour
{
    // Speed of rotation in degrees per second
    public float rotationSpeed = 360.0f; // Adjust this value in the Unity Inspector to change the speed

    void Update()
    {
        // Rotate around the Y-axis at the specified speed
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}


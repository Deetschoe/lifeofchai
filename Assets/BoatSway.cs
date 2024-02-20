using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSway : MonoBehaviour
{
    public float swayAmount = 2f; // Max sway amount in degrees
    public float swaySpeed = 2f; // How fast the sway moves

    private float swayFactor = 0f; // Keeps track of the current sway offset

    void Update()
    {
        // Calculate the sway factor over time, oscillating between -1 and 1
        swayFactor = Mathf.Sin(Time.time * swaySpeed);

        // Apply the sway rotation to the boat around the up axis (y-axis)
        transform.localRotation = Quaternion.Euler(0f, 0f, swayFactor * swayAmount);
    }
}


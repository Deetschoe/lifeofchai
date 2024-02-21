using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    public float startY = -30f;
    public float endY = 517f;
    public float duration = 3f; // Adjust the duration as needed

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;
        t = Mathf.Clamp01(t); // Ensure t stays between 0 and 1

        // Interpolate the Y position between startY and endY
        float newY = Mathf.Lerp(startY, endY, t);

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Check if the movement is complete
        if (t >= 1.0f)
        {
            // Movement is complete, you can perform additional actions here if needed
            Debug.Log("Movement Complete");
        }
    }
}

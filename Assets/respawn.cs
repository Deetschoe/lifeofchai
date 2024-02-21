using UnityEngine;

public class RespawnOnStop : MonoBehaviour
{
    public float movementThreshold = 0.01f; // Minimum movement threshold to consider as "stopped"
    public float checkInterval = 1f; // How often to check for movement, in seconds
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rb;
    private float lastCheckTime = 0;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Time.time - lastCheckTime >= checkInterval)
        {
            lastCheckTime = Time.time;
            CheckMovementAndRespawn();
        }
    }

    private void CheckMovementAndRespawn()
    {
        if (rb.velocity.magnitude < movementThreshold && rb.angularVelocity.magnitude < movementThreshold)
        {
            // NPC has stopped moving, reset its position and rotation
            rb.position = startPosition;
            rb.rotation = startRotation;

            // Optionally reset velocity if needed
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}

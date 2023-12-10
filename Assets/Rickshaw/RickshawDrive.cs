using UnityEngine;

public class RickshadwDrive : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;
    public float turnSpeed = 1.0f;
    public float maxBounceHeight = 0.5f; // Maximum possible height of the bounce
    public float bounceSpeed = 2.0f; // Speed of the bouncing

    private int currentWaypointIndex = 0;
    private float originalY; // Original y position of the car
    private float currentBounceHeight; // Current height of the bounce

    void Start()
    {
        originalY = transform.position.y;
        SetRandomBounceHeight();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
                SetRandomBounceHeight(); // Set a new bounce height when waypoint changes
            }
        }

        Vector3 moveDirection = waypoints[currentWaypointIndex].position - transform.position;
        moveDirection.y = 0; // Ignore the y component for movement towards waypoint
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, speed * Time.deltaTime);

        // Bounce effect with variable height
        float newY = originalY + Mathf.Sin(Time.time * bounceSpeed) * currentBounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Smooth turning
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void SetRandomBounceHeight()
    {
        // Randomize the bounce height within a range, e.g., 50% to 100% of maxBounceHeight
        currentBounceHeight = Random.Range(maxBounceHeight * 0.5f, maxBounceHeight);
    }
}

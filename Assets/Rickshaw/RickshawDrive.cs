using UnityEngine;

public class RickshadwDrive : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;
    public float turnSpeed = 1.0f; // New variable for turn speed

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

        // New code for smooth turning
        Quaternion targetRotation = Quaternion.LookRotation(waypoints[currentWaypointIndex].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}

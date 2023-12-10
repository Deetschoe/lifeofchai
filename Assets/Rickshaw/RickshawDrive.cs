using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;

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
        transform.LookAt(waypoints[currentWaypointIndex].position);
    }
}

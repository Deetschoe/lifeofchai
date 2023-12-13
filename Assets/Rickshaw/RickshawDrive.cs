using UnityEngine;
using System.Collections;

public class RickshawDrive : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5.0f;
    public float increasedSpeed = 10.0f; // New speed after contact
    public float turnSpeed = 1.0f;
    public float maxBounceHeight = 0.5f;
    public float bounceSpeed = 2.0f;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject stopButton;

    private int currentWaypointIndex = 0;
    private float originalY;
    private float currentBounceHeight;
    private bool speedIncreased = false; // Flag to indicate if speed has been increased
    private float proximityThreshold = 0.15f; // Threshold for hand proximity

    void Start()
    {
        originalY = transform.position.y;
        SetRandomBounceHeight();
    }

    void Update()
    {
        if (!speedIncreased && (Vector3.Distance(leftHand.transform.position, stopButton.transform.position) < proximityThreshold ||
            Vector3.Distance(rightHand.transform.position, stopButton.transform.position) < proximityThreshold))
        {
            StartCoroutine(IncreaseSpeedAfterDelay(6.0f)); // Increase speed after 6 seconds delay
            speedIncreased = true;
        }

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
                SetRandomBounceHeight();
            }
        }

        Vector3 moveDirection = waypoints[currentWaypointIndex].position - transform.position;
        moveDirection.y = 0;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + moveDirection, speed * Time.deltaTime);

        float newY = originalY + Mathf.Sin(Time.time * bounceSpeed) * currentBounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private IEnumerator IncreaseSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        speed = increasedSpeed;
    }

    private void SetRandomBounceHeight()
    {
        currentBounceHeight = Random.Range(maxBounceHeight * 0.5f, maxBounceHeight);
    }
}

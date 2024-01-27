using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float detectionDistance = 2.0f; // Distance to check for potential collisions
    private Vector3 startPosition;
    private Vector3 randomDirection;
    private float changeDirectionTime;
    private bool isPausing = false;

    void Start()
    {
        startPosition = transform.position;
        ChooseNewDirection();
    }

    void Update()
    {
        // Move the object
        MoveObject();

        // Check if it's time to change direction
        if (Time.time >= changeDirectionTime)
        {
            ChooseNewDirection();
        }

        // Randomly decide to pause
        if (!isPausing && Random.Range(0f, 1f) < 0.001f) // 10% chance to pause
        {
            StartCoroutine(PauseMovement());
        }
    }

    void MoveObject()
    {
        // Check for potential collisions
        RaycastHit hit;
        if (Physics.Raycast(transform.position, randomDirection, out hit, detectionDistance))
        {
            ChooseNewDirection();
        }
        else
        {
            transform.Translate(randomDirection * moveSpeed * Time.deltaTime);

            // Keep the object within the 5x5 radius
            if (Vector3.Distance(startPosition, transform.position) > 12.5f)
            {
                transform.position = startPosition + (transform.position - startPosition).normalized * 12.5f;
                ChooseNewDirection();
            }
        }
    }

    void ChooseNewDirection()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        changeDirectionTime = Time.time + Random.Range(3f, 12f);
    }

    IEnumerator PauseMovement()
    {
        isPausing = true;
        float currentSpeed = moveSpeed;
        moveSpeed = 0;
        yield return new WaitForSeconds(Random.Range(0.1f, 1f));
        moveSpeed = currentSpeed;
        isPausing = false;
    }
}

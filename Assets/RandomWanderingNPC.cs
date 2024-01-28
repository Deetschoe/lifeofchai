using System.Collections;
using UnityEngine;

public class RandomWanderingNPC : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float detectionDistance = 2.0f;
    private Vector3 startPosition;
    private Vector3 randomDirection;
    private float changeDirectionTime;
    private bool isPausing = false;

    private Animator animator; // Reference to the Animator component

    void Start()
    {
        startPosition = transform.position;
        ChooseNewDirection();
        animator = GetComponent<Animator>(); // Initialize the Animator component
    }
    void Update()
    {
        MoveObject();

        if (Time.time >= changeDirectionTime)
        {
            ChooseNewDirection();
        }

        // Increase the chance of pausing for visibility
        if (!isPausing && Random.Range(0f, 1f) < 0.0005f) // 5% chance to pause
        {
            StartCoroutine(PauseMovement());
        }

        // Update animation state
        animator.SetBool("IsMoving", moveSpeed > 0 && !isPausing);
    }

    IEnumerator PauseMovement()
    {
        isPausing = true;
        float currentSpeed = moveSpeed;
        moveSpeed = 0;

        // Update animation state to idle
        animator.SetBool("IsMoving", false);

        yield return new WaitForSeconds(Random.Range(0.1f, 1f));

        moveSpeed = currentSpeed;
        isPausing = false;

        // Update animation state to moving (if applicable)
        animator.SetBool("IsMoving", moveSpeed > 0);
    }


    void MoveObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, randomDirection, out hit, detectionDistance))
        {
            ChooseNewDirection();
        }
        else
        {
            transform.Translate(randomDirection * moveSpeed * Time.deltaTime);

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

        // Rotate to face the new direction
        transform.rotation = Quaternion.LookRotation(randomDirection);
    }


}

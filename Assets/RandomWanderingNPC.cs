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

        if (!isPausing && Random.Range(0f, 1f) < 0.005f) // 5% chance to pause
        {
            StartCoroutine(PauseMovement());
        }

        animator.SetBool("IsMoving", moveSpeed > 0 && !isPausing);
    }

    IEnumerator PauseMovement()
    {
        isPausing = true;
        float currentSpeed = moveSpeed;
        moveSpeed = 0;

        animator.SetBool("IsMoving", false);

        yield return new WaitForSeconds(Random.Range(0.1f, 1f));

        moveSpeed = currentSpeed;
        isPausing = false;

        animator.SetBool("IsMoving", moveSpeed > 0);
    }

    void MoveObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance) && hit.collider.CompareTag("wall"))
        {
            // Wall detected, move away from the wall
            MoveAwayFromWall(hit);
        }
        else
        {
            // No wall, proceed with movement
            transform.Translate(randomDirection * moveSpeed * Time.deltaTime, Space.World);

            if (Vector3.Distance(startPosition, transform.position) > 12.5f)
            {
                transform.position = startPosition + (transform.position - startPosition).normalized * 12.5f;
                ChooseNewDirection();
            }
        }
    }

    void MoveAwayFromWall(RaycastHit hit)
    {
        Vector3 awayFromWall = transform.position - hit.point;
        awayFromWall.y = 0; // Ensure movement is only in the horizontal plane
        randomDirection = awayFromWall.normalized;

        transform.rotation = Quaternion.LookRotation(randomDirection);
    }

    void ChooseNewDirection()
    {
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        changeDirectionTime = Time.time + Random.Range(3f, 12f);

        transform.rotation = Quaternion.LookRotation(randomDirection);
    }
}

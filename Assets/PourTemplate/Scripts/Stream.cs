using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    private ParticleSystem splashParticle = null;

    private Coroutine pourRoutine = null;


    private Vector3 targetPosition = Vector3.zero;

    private GameObject streamColliderObject = null; // New GameObject for collider



    private void Awake()
    {
        gameObject.tag = "chaiRay";


        // Create the collider object and add a collider to it
        streamColliderObject = new GameObject("StreamCollider");
        streamColliderObject.transform.parent = transform; // Set as child of the stream object
        BoxCollider collider = streamColliderObject.AddComponent<BoxCollider>();
        collider.isTrigger = true; // Set as trigger to detect collisions without physical impact

        // Optionally, add Rigidbody and set it to kinematic if needed
        Rigidbody rb = streamColliderObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        rb.useGravity = false;

        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();


    }

    private void UpdateCollider()
    {
        // Update the collider position and rotation to match the stream
        if (lineRenderer.positionCount > 0)
        {
            Vector3 startPosition = lineRenderer.GetPosition(0);
            Vector3 endPosition = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

            streamColliderObject.transform.position = Vector3.Lerp(startPosition, endPosition, 0.5f);
            streamColliderObject.transform.LookAt(endPosition);

            // Adjust collider size based on the stream length
            BoxCollider collider = streamColliderObject.GetComponent<BoxCollider>();
            collider.size = new Vector3(collider.size.x, collider.size.y, Vector3.Distance(startPosition, endPosition));
        }
    }

    private void Update()
    {
        UpdateCollider();
    }

    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while (gameObject.activeSelf)
        {
            targetPosition = FindEndPoint();

            MoveToPosition(0, transform.position);
            AnimateToPosition(1, targetPosition);

            yield return null;

        }
    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());

    }

    private IEnumerator EndPour()
    {

        while(!HasReachedPosition(0, targetPosition))
        {
            AnimateToPosition(0, targetPosition);
            AnimateToPosition(1, targetPosition);

            yield return null;


        }
        Destroy(gameObject);
    }

    private Vector3 FindEndPoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 2.0f);

        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);

        return endPoint;
    }

    private void MoveToPosition(int index, Vector3 targetPosition)
    {
        lineRenderer.SetPosition(index, targetPosition);
    }

    private void AnimateToPosition(int index, Vector3 targetPosition)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);

        lineRenderer.SetPosition(index, newPosition);
    }

    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {

        Vector3 currentPosition = lineRenderer.GetPosition(index);

        return currentPosition == targetPosition;
    }

    private IEnumerator UpdateParticle()
    {

        while(gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = targetPosition;

            bool isHitting = HasReachedPosition(1, targetPosition);
            splashParticle.gameObject.SetActive(isHitting);

            yield return null;

        }

    }

}


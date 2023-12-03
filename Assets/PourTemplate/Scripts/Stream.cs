using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    private LineRenderer lineRenderer = null;
    private ParticleSystem splashParticle = null;

    private Coroutine pourRoutine = null;


    private Vector3 targetPosition = Vector3.zero;




    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();


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
<<<<<<< HEAD
        while (!HasReachedPosition(0, targetPosition))
=======
        while(!HasReachedPosition(0, targetPosition))
>>>>>>> 2b54cc8a42a66eeffdea9f7ef7f01bde9b83f0ac
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

<<<<<<< HEAD
    private bool HasReachedPosition(int index, Vector3 targetPosition)
    {
=======
    private bool HasReachedPosition( int index, Vector3 targetPosition) {
>>>>>>> 2b54cc8a42a66eeffdea9f7ef7f01bde9b83f0ac
        Vector3 currentPosition = lineRenderer.GetPosition(index);

        return currentPosition == targetPosition;
    }

    private IEnumerator UpdateParticle()
    {
<<<<<<< HEAD
        while (gameObject.activeSelf)
=======
        while(gameObject.activeSelf)
>>>>>>> 2b54cc8a42a66eeffdea9f7ef7f01bde9b83f0ac
        {
            splashParticle.gameObject.transform.position = targetPosition;

            bool isHitting = HasReachedPosition(1, targetPosition);
            splashParticle.gameObject.SetActive(isHitting);

            yield return null;

        }

    }

<<<<<<< HEAD
}
=======
}
>>>>>>> 2b54cc8a42a66eeffdea9f7ef7f01bde9b83f0ac

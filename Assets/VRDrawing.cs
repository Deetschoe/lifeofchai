using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FingerTipPencil : MonoBehaviour
{
    public GameObject drawingPlane; // The plane where the drawing will appear
    public GameObject pencilTip; // A small sphere attached to the fingertip for drawing
    public Material drawingMaterial; // Material for the line renderer
    public float drawingDistance = 0.05f; // Maximum distance from the plane at which drawing starts
    public GameObject[] corners; // Array of 4 GameObjects representing each corner of the paper
    private bool isSayingShape = false;
    private Vector3 paperMidpoint;

    private LineRenderer currentLine;
    private bool isDrawing = false;
    private Transform planeTransform;
    private Bounds paperBounds;
    public AudioClip drawCircleClip;
    public AudioClip drawSquareClip;
    private AudioSource audioSource;

    private bool shouldDrawCircle;
    public AudioClip wrongDrawingClip; // AudioClip for wrong side drawing
    void CalculatePaperMidpoint()
    {
        if (corners.Length != 4)
        {
            Debug.LogError("Four corner objects required");
            return;
        }

        Vector3 sumOfCorners = Vector3.zero;
        foreach (var corner in corners)
        {
            sumOfCorners += corner.transform.position;
        }
        paperMidpoint = sumOfCorners / corners.Length;
    }

    bool CheckDrawingSide()
    {
        if (!currentLine)
        {

            Debug.Log("no line");
            return false;
        }

        // Calculate average position of the line to determine its general location
        Vector3 averagePosition = Vector3.zero;

        for (int i = 0; i < currentLine.positionCount; i++)
        {
            averagePosition += currentLine.GetPosition(i);
        }
        averagePosition /= currentLine.positionCount;
        Debug.Log(paperMidpoint.x);
        Debug.Log(averagePosition.x);

        if (shouldDrawCircle)
        {
            return averagePosition.x > paperMidpoint.x; // Circle should be on the right
        }
        else
        {
            return averagePosition.x < paperMidpoint.x; // Square should be on the left
        }
    }

    void Start()
    {
        planeTransform = drawingPlane.transform;
        CalculatePaperBounds();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator WrongShape()
    {
        audioSource.PlayOneShot(wrongDrawingClip);

        yield return new WaitForSeconds(5); // Wait for 5 seconds
        isSayingShape = false; // Set the variable to false after 5 seconds

    }

    IEnumerator CircleOrSquare()
    {
        shouldDrawCircle = Random.value > 0.5f;
        if (shouldDrawCircle)
        {
            audioSource.PlayOneShot(drawCircleClip);
        }
        else
        {
            audioSource.PlayOneShot(drawSquareClip);
        }

        yield return new WaitForSeconds(3); // Wait for 5 seconds
        isSayingShape = false; // Set the variable to false after 5 seconds
    }
    void CalculatePaperBounds()
    {
        if (corners.Length != 4)
        {
            Debug.LogError("Four corner objects required");
            return;
        }

        paperBounds = new Bounds(corners[0].transform.position, Vector3.zero);
        foreach (var corner in corners)
        {
            paperBounds.Encapsulate(corner.transform.position);
        }
    }

    void Update()
    {
        Vector3 planeNormal = planeTransform.up; // Assuming the plane's 'up' is its normal
        float distanceToPlane = Vector3.Dot(pencilTip.transform.position - planeTransform.position, planeNormal);
        distanceToPlane = Mathf.Abs(distanceToPlane);


        if (distanceToPlane <= drawingDistance && IsWithinPaperBounds(pencilTip.transform.position))
        {
            if (!isDrawing)
            {
                StartDrawing();
            }
            Draw();
        }
        else
        {
            if (isDrawing)
            {
                StopDrawing();
            }
        }
    }

    bool IsWithinPaperBounds(Vector3 position)
    {
        return paperBounds.Contains(new Vector3(position.x, corners[0].transform.position.y, position.z));
    }

    void StartDrawing()
    {
        CreateNewLine();
        isDrawing = true;
    }

    void Draw()
    {
        Vector3 currentPosition = pencilTip.transform.position;
        currentPosition.y = corners[0].transform.position.y; // Set the Y position to the Y of the first corner

        if (currentLine != null && currentPosition != currentLine.GetPosition(currentLine.positionCount - 1) && IsWithinPaperBounds(currentPosition))
        {
            UpdateLine(currentPosition);
        }
    }

    void CreateNewLine()
    {
        currentLine = new GameObject("Line").AddComponent<LineRenderer>();
        currentLine.material = drawingMaterial;
        currentLine.positionCount = 1;
        Vector3 startPosition = pencilTip.transform.position;
        startPosition.y = corners[0].transform.position.y; // Set the Y position to the Y of the first corner
        currentLine.SetPosition(0, startPosition);
        currentLine.startWidth = 0.01f;
        currentLine.endWidth = 0.01f;
    }

    void UpdateLine(Vector3 newPosition)
    {
        currentLine.positionCount++;
        newPosition.y = corners[0].transform.position.y; // Set the Y position to the Y of the first corner
        currentLine.SetPosition(currentLine.positionCount - 1, newPosition);
    }
    void StopDrawing()
    {
        if (isDrawing)
        {
            bool drawnOnCorrectSide = CheckDrawingSide();
            if (!drawnOnCorrectSide)
            {
                isSayingShape = true;

                StartCoroutine(WrongShape());

            }
            else
            {
                if (!isSayingShape)
                {
                    isSayingShape = true;
                    StartCoroutine(CircleOrSquare());
                }
            }
        }  






        currentLine = null;


        isDrawing = false;
        // Clear the drawing
        if (currentLine)
        {
            Destroy(currentLine.gameObject);
        }




    }


}

using UnityEngine;

public class LiquidControl : MonoBehaviour
{
    public GameObject liquidObject; // Assign the liquid object in the Inspector
    public float liquidLevel = 0.5f; // Default level, can be changed in Inspector or via other scripts

    private const float MaxLiquidLevel = 5.5f; // Maximum liquid level
    private float timeSinceLastIncrement = 0f;
    private const float IncrementInterval = 0.01f; // Time in seconds to increment liquid level
    private const float ProximityThreshold = 0.2f; // Threshold distance to increase liquid level

    void Update()
    {
        // Adjust the liquid level and ensure it does not exceed the maximum
        liquidLevel = Mathf.Min(liquidLevel, MaxLiquidLevel);

        // Scale the liquid object based on the liquid level
        Vector3 scale = liquidObject.transform.localScale;
        scale.y = liquidLevel;
        liquidObject.transform.localScale = scale;

        // Adjust the position to anchor the bottom
        liquidObject.transform.localPosition = new Vector3(0, scale.y + 1f, 0);

        // Find all objects with the tag "chaiRay"
        GameObject[] chaiRayObjects = GameObject.FindGameObjectsWithTag("chaiRay");

        // Check proximity with each chaiRay object
        foreach (GameObject chaiRayObject in chaiRayObjects)
        {
            // Assuming chaiRayObject uses a LineRenderer
            LineRenderer lineRenderer = chaiRayObject.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                // Get the bottom position of the chaiRay (last point of the LineRenderer)
                Vector3 bottomPosition = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                print((Vector3.Distance(transform.position, bottomPosition)));

                // Check the distance from the bottom of the chaiRay to the top of the cup
                if (Vector3.Distance(transform.position, bottomPosition) <= ProximityThreshold)
                {
                    if (timeSinceLastIncrement >= IncrementInterval)
                    {
                        liquidLevel += 0.03f;
                        timeSinceLastIncrement = 0f;
                    }
                }
            }
        }


        // Increment the timer
        timeSinceLastIncrement += Time.deltaTime;
    }
}

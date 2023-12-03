using UnityEngine;

public class LiquidControl : MonoBehaviour
{
    public GameObject liquidObject; // Assign the liquid object in the Inspector
    public float liquidLevel = 0.5f; // Default level, can be changed in Inspector or via other scripts

    private const float MaxLiquidLevel = 10f; // Maximum liquid level
    private float timeSinceLastIncrement = 0f;
    private const float IncrementInterval = 1f; // Time in seconds to increment liquid level
    private const float ProximityThreshold = 1f; // Threshold distance to increase liquid level

    void Update()
    {
        // Adjust the liquid level and ensure it does not exceed the maximum
        liquidLevel = Mathf.Min(liquidLevel, MaxLiquidLevel);

        // Scale the liquid object based on the liquid level
        Vector3 scale = liquidObject.transform.localScale;
        scale.y = liquidLevel;
        liquidObject.transform.localScale = scale;

        // Adjust the position to anchor the bottom
        liquidObject.transform.localPosition = new Vector3(0, scale.y / 2, 0);

        // Find all objects with the tag "chaiRay"
        GameObject[] chaiRayObjects = GameObject.FindGameObjectsWithTag("chaiRay");

        // Check proximity with each chaiRay object
        foreach (GameObject chaiRayObject in chaiRayObjects)
        {
            if (Vector3.Distance(transform.position, chaiRayObject.transform.position) <= ProximityThreshold)
            {
                if (timeSinceLastIncrement >= IncrementInterval)
                {
                    liquidLevel += 0.1f;
                    timeSinceLastIncrement = 0f;
                }
            }
        }

        // Increment the timer
        timeSinceLastIncrement += Time.deltaTime;
    }
}

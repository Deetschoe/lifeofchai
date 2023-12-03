using UnityEngine;

public class LiquidControl : MonoBehaviour
{
    public GameObject liquidObject; // Assign the liquid object in the Inspector
    public float liquidLevel = 0.5f; // Default level, can be changed in Inspector or via other scripts

    public GameObject proximityObject1; // First object to check proximity
    public GameObject proximityObject2; // Second object to check proximity

    private float timeSinceLastIncrement = 0f;
    private const float IncrementInterval = 1f; // Time in seconds to increment liquid level
    private const float ProximityThreshold = 1f; // Threshold distance to increase liquid level

    void Update()
    {
        // Adjust the liquid level
        Vector3 scale = liquidObject.transform.localScale;
        scale.y = liquidLevel;
        liquidObject.transform.localScale = scale;

        liquidObject.transform.localPosition = new Vector3(0, liquidLevel / 2, 0);

        print(Vector3.Distance(proximityObject1.transform.position, proximityObject2.transform.position));

        // Check proximity and increment liquid level
        if (Vector3.Distance(proximityObject1.transform.position, proximityObject2.transform.position) <= ProximityThreshold)
        {
            if (timeSinceLastIncrement >= IncrementInterval)
            {
                liquidLevel += 0.1f;
                timeSinceLastIncrement = 0f;
            }
        }

        // Increment the timer
        timeSinceLastIncrement += Time.deltaTime;
    }
}

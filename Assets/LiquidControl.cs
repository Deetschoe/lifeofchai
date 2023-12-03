using UnityEngine;

public class LiquidControl : MonoBehaviour
{
    public GameObject liquidObject; // Assign the liquid object in the Inspector
    public float liquidLevel = 0.5f; // Default level, can be changed in Inspector or via other scripts

    public GameObject touchingObject1; // First object to detect collision
    public GameObject touchingObject2; // Second object to detect collision

    private float timeSinceLastIncrement = 0f;
    private const float IncrementInterval = 1f; // Time in seconds to increment liquid level

    void Update()
    {
        // Adjust the liquid level
        Vector3 scale = liquidObject.transform.localScale;
        scale.y = liquidLevel;
        liquidObject.transform.localScale = scale;

        liquidObject.transform.localPosition = new Vector3(0, liquidLevel / 2, 0);
    }

    void OnTriggerStay(Collider other)
    {
        // Check if the colliding objects are the ones we're interested in
        if ((other.gameObject == touchingObject1 || other.gameObject == touchingObject2) && timeSinceLastIncrement >= IncrementInterval)
        {
            liquidLevel += 0.1f;
            timeSinceLastIncrement = 0f;
        }
    }

    void FixedUpdate()
    {
        // Increment the timer
        timeSinceLastIncrement += Time.fixedDeltaTime;
    }
}

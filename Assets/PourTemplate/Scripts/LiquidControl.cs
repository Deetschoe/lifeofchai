using UnityEngine;

public class LiquidControl : MonoBehaviour
{
    public GameObject liquidObject; // Assign the liquid object in the Inspector
    public float liquidLevel = 0.5f; // Default level, can be changed in Inspector or via other scripts

    private const float MaxLiquidLevel = 10.0f; // Define the maximum liquid level
    private const float IncrementAmount = 1.0f; // Amount to increment the liquid level

    void Update()
    {
        // Adjust the liquid level
        Vector3 scale = liquidObject.transform.localScale;
        scale.y = liquidLevel;
        liquidObject.transform.localScale = scale;
        liquidObject.transform.localPosition = new Vector3(0, liquidLevel / 2, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "chaiRay" tag
        if (other.CompareTag("chaiRay"))
        {
            // Increase the liquid level
            liquidLevel += IncrementAmount;
            // Clamp the liquid level to the maximum value
            liquidLevel = Mathf.Min(liquidLevel, MaxLiquidLevel);
        }
    }
}

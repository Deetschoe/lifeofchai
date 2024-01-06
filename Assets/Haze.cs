using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DistanceFog : MonoBehaviour
{
    public Color fogColor = Color.grey; // Set the color of the fog
    public float fogStartDistance = 10f; // The distance where the fog will start
    public float fogEndDistance = 20f; // The distance where the fog will be thickest

    private void Start()
    {
        RenderSettings.fog = true; // Enable fog
        RenderSettings.fogColor = fogColor; // Set the fog color
        RenderSettings.fogMode = FogMode.Linear; // Set the fog to linear mode
        RenderSettings.fogStartDistance = fogStartDistance; // Set the start distance of the fog
        RenderSettings.fogEndDistance = fogEndDistance; // Set the end distance of the fog
    }
}

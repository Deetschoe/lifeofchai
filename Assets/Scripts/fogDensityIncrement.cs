using UnityEngine;
using System.Collections;

public class FogController : MonoBehaviour
{
    public float targetFogDensity = 0.1f; // The target density of the fog
    public float duration = 45.0f; // Duration over which the fog density increases

    void Start()
    {
        // Set initial fog settings
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogColor = Color.gray;
        RenderSettings.fogDensity = 0.10f; // Set your preferred starting fog density

        // Start the coroutine to increase fog density
        StartCoroutine(IncreaseFogDensity());
    }

    IEnumerator IncreaseFogDensity()
    {
        float startDensity = RenderSettings.fogDensity;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(startDensity, targetFogDensity, elapsedTime / duration);
            yield return null;
        }
    }
}

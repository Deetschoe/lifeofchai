using UnityEngine;
using System.Collections;

public class EnvironmentAndParticleSystemController : MonoBehaviour
{
    // Fog settings
    public float targetFogDensity = 0.1f; // The target density of the fog
    public float fogDuration = 45.0f; // Duration over which the fog density increases

    // Particle System settings
    public ParticleSystem targetParticleSystem; // Reference to your particle system
    public float maxStartLifetime = 5.0f; // The target start lifetime for the particles
    public int maxParticles = 1000; // Maximum number of particles
    public float maxSimulationSpeed = 100f; // Maximum simulation speed


    public float targetAmbientLightIntensity = 0.0f; // The target ambient light intensity (complete darkness)
    public float ambientLightDuration = 30.0f; // Duration over which ambient light intensity decreases

    private float initialFogDensity;
    private float initialStartLifetime;
    private int initialMaxParticles;
    private float initialSimulationSpeed;

    void Start()
    {
        // Initialize fog settings
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogColor = Color.gray;
        RenderSettings.fogDensity = 0.02f; // Set your preferred starting fog density

        if (targetParticleSystem != null)
        {
            var mainModule = targetParticleSystem.main;
            initialStartLifetime = mainModule.startLifetime.constant;
            initialMaxParticles = mainModule.maxParticles;
            initialSimulationSpeed = mainModule.simulationSpeed;
        }

        // Start the coroutine to increase fog density and adjust particle system
        StartCoroutine(IncreaseFogDensityAndAdjustParticleSystem());
    }

    IEnumerator IncreaseFogDensityAndAdjustParticleSystem()
    {
        initialFogDensity = RenderSettings.fogDensity;
        float elapsedTime = 0;

        while (elapsedTime < fogDuration)
        {
            elapsedTime += Time.deltaTime;
            float newDensity = Mathf.Lerp(initialFogDensity, targetFogDensity, elapsedTime / fogDuration);
            RenderSettings.fogDensity = newDensity;

            if (targetParticleSystem != null)
            {
                var mainModule = targetParticleSystem.main;
                mainModule.startLifetime = Mathf.Lerp(initialStartLifetime, maxStartLifetime, elapsedTime / fogDuration);
                mainModule.maxParticles = (int)Mathf.Lerp(initialMaxParticles, maxParticles, elapsedTime / fogDuration);
                mainModule.simulationSpeed = Mathf.Lerp(initialSimulationSpeed, maxSimulationSpeed, elapsedTime / fogDuration);
            }

            yield return null;
        }
    }
}

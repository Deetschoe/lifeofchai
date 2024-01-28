using UnityEngine;
using System.Collections; // This is the missing namespace

public class BallSpawnCreature : MonoBehaviour
{
    public GameObject creaturePrefab; // Assign your creature prefab here
    public float animationDuration = 2.0f; // Duration of the spawn animation
    private Rigidbody rb;
    private bool hasCollided = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }
    }

    void Update()
    {
        if (!hasCollided)
        {
            AdjustGravity();
        }
    }

    private void AdjustGravity()
    {
        if (rb.velocity.y < 0) // Only reduce gravity when the ball is falling
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (0.5f - 1) * Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object has the tag "ground"
        if (collision.gameObject.CompareTag("ground") && !hasCollided)
        {
            hasCollided = true;

            rb.useGravity = false;
            rb.isKinematic = true;

            Invoke(nameof(SpawnCreature), 1.0f); // Delay for creature spawning
            Invoke(nameof(DeactivateBall), 3.0f); // Delay for ball removing
        }
    }



    private void DeactivateBall()
    {
        gameObject.SetActive(false);
    }
    private void SpawnCreature()
    {
        GameObject creature = Instantiate(creaturePrefab, transform.position, Quaternion.identity);

        // Calculate half height of the creature
        Renderer creatureRenderer = creature.GetComponent<Renderer>();
        float halfHeight = 0;
        if (creatureRenderer != null)
        {
            halfHeight = creatureRenderer.bounds.size.y / 2;
        }
        else
        {
            // Alternatively, use collider if renderer is not available
            Collider creatureCollider = creature.GetComponent<Collider>();
            if (creatureCollider != null)
            {
                halfHeight = creatureCollider.bounds.size.y / 2;
            }
        }

        // Adjust the Y position
        Vector3 adjustedPosition = creature.transform.position;
        adjustedPosition.y = 0;
        adjustedPosition.y += halfHeight;
        creature.transform.position = adjustedPosition;

        // Start scaling coroutine
        StartCoroutine(ScaleCreature(creature, Vector3.zero, Vector3.one, animationDuration));

    }


    private IEnumerator ScaleCreature(GameObject creature, Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            creature.transform.localScale = Vector3.Lerp(startScale, endScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        creature.transform.localScale = endScale;
    }
}

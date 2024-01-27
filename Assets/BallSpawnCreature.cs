using UnityEngine;
using System.Collections;

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
        // Check if the ball collides with an object tagged as "ground"
        if (!hasCollided && collision.gameObject.CompareTag("ground"))
        {
            hasCollided = true;
            rb.useGravity = false;
            rb.isKinematic = true;
            Invoke(nameof(SpawnCreature), 1.0f); // Delay for creature spawning
        }
    }

    private void SpawnCreature()
    {
        GameObject creature = Instantiate(creaturePrefab, transform.position, Quaternion.identity);
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

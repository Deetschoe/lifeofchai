using UnityEngine;
using System.Collections;

public class MoveTowardsUntamed : MonoBehaviour
{
    public float speed = 5f;
    public float stoppingDistance = 1f;
    public float attackCooldown = 1f;
    private float timeSinceLastAttack = 0f;

    private GameObject FindClosestUntamed()
    {
        GameObject[] untamedObjects;
        untamedObjects = GameObject.FindGameObjectsWithTag("untamed");
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject untamed in untamedObjects)
        {
            Vector3 directionToUntamed = untamed.transform.position - currentPosition;
            float distanceToUntamed = directionToUntamed.sqrMagnitude;
            if (distanceToUntamed < minDistance)
            {
                closest = untamed;
                minDistance = distanceToUntamed;
            }
        }

        return closest;
    }

    void Update()
    {
        GameObject closestUntamed = FindClosestUntamed();
        if (closestUntamed != null)
        {
            float step = speed * Time.deltaTime;
            Vector3 targetPosition = closestUntamed.transform.position;

            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                timeSinceLastAttack = 0; // Reset attack timer when not in range
            }
            else
            {
                if (timeSinceLastAttack >= attackCooldown)
                {
                    Attack(closestUntamed);
                    timeSinceLastAttack = 0; // Reset timer after attack
                }
                else
                {
                    timeSinceLastAttack += Time.deltaTime;
                }
            }
        }
    }

    void Attack(GameObject target)
    {
        HealthBarGradient health = target.GetComponent<HealthBarGradient>();
        if (health != null)
        {
            int damage = Random.Range(0, 21); // Generates a random integer between 0 and 20
            health.currentHealth -= damage;
            if (health.currentHealth < 0)
            {
                target.SetActive(false);
            }
            // Add any additional effects or checks here, e.g., if health reaches 0
        }
    }
}

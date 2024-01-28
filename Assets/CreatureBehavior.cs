using UnityEngine;
using System.Collections;

public class MoveTowardsUntamed : MonoBehaviour
{
    public float speed = 5f;
    public float stoppingDistance = 1f;
    public float attackCooldown = 1f;
    private float timeSinceLastAttack = 0f;
    private int level = 1; // Initial level
    private EntityLabelUpdater entityLabelUpdater;
    void Start()
    {
        // Get the EntityLabelUpdater component from the same GameObject
        entityLabelUpdater = GetComponent<EntityLabelUpdater>();
        if (entityLabelUpdater == null)
        {
            Debug.LogError("EntityLabelUpdater component not found on the GameObject.");
        }
    }
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
        // Update properties based on level
        UpdatePropertiesBasedOnLevel();

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


    void UpdatePropertiesBasedOnLevel()
    {
        speed = level;
        stoppingDistance = level;
        attackCooldown = 1f / level;
    }

    void Attack(GameObject target)
    {

        HealthBarGradient health = target.GetComponent<HealthBarGradient>();
        if (health != null)
        {
            int maxDamage = level * 10;
            int damage = Random.Range(0, maxDamage);
            health.currentHealth -= damage;

            if (health.currentHealth < 0)
            {
                target.SetActive(false);
                IncrementLevel(); // Increment level on successful elimination
            }
            // Add any additional effects or checks here
        }
    }

    void IncrementLevel()
    {
        level++;
        UpdateEntityLevelInLabelUpdater(); // Update EntityLevel in EntityLabelUpdater
    }

    void UpdateEntityLevelInLabelUpdater()
    {
        if (entityLabelUpdater != null)
        {
            // Assuming EntityLabelUpdater has a method or property to set the EntityLevel
            entityLabelUpdater.entityLevel = level;
        }
    }
}

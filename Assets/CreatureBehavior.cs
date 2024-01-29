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
    public Animator externalAnimator;

    // Additions for audio
    public AudioClip[] attackSounds; // Array of audio clips
    private AudioSource audioSource; // AudioSource component

    // Animator component
    private Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Ensure attackSounds array is not null
        if (attackSounds == null)
        {
            attackSounds = new AudioClip[0];
            Debug.LogWarning("Attack sounds array is not initialized. No sounds will be played during attacks.");
        }

        // Get the EntityLabelUpdater component from the same GameObject
        entityLabelUpdater = GetComponent<EntityLabelUpdater>();
        if (entityLabelUpdater == null)
        {
            Debug.LogError("EntityLabelUpdater component not found on the GameObject.");
        }

        if (externalAnimator == null)
        {
            externalAnimator = GetComponent<Animator>();
            if (externalAnimator == null)
            {
                Debug.LogError("Animator component not found on the GameObject, and no external animator assigned.");
            }
        }
    }

    private GameObject FindClosestUntamed()
    {
        GameObject[] untamedObjects = GameObject.FindGameObjectsWithTag("untamed");
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
                    print(closestUntamed);
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
        speed = level + 1;
        attackCooldown = 1f / (level + 1);
    }

    void Attack(GameObject target)
    {
        HealthBarGradient health = target.GetComponent<HealthBarGradient>();

        if (health != null)
        {
            if (attackSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, attackSounds.Length);
                audioSource.clip = attackSounds[randomIndex];
                audioSource.Play();
            }

            StartCoroutine(AttackRoutine());

            int maxDamage = level * 10;
            int damage = Random.Range(0, maxDamage);
            health.currentHealth -= damage;
            print(health.currentHealth);
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
            entityLabelUpdater.entityLevel = level;
        }
    }

    private IEnumerator AttackRoutine()
    {
        print("Start attacking");
        StartAttackingAnimation(); // Start the attack animation

        yield return new WaitForSeconds(2.5f); // Wait for 2.5 seconds
        print("styttacking");

        StopAttackingAnimation(); // Stop the attack animation
    }

    void StartAttackingAnimation()
    {
        if (externalAnimator != null)
        {
            externalAnimator.SetBool("IsAttacking", true);
        }
    }

    void StopAttackingAnimation()
    {
        if (externalAnimator != null)
        {
            externalAnimator.SetBool("IsAttacking", false);
        }
    }
}

using UnityEngine;
using UnityEngine.AI; // Make sure to include this namespace

public class NPCChase : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the NPC moves (might not be needed if using NavMeshAgent)
    private Transform target; // Target to chase
    private NavMeshAgent agent; // Reference to the NavMeshAgent component

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        if (agent == null) // Check if NavMeshAgent is attached
        {
            Debug.LogError("NavMeshAgent component is missing on " + gameObject.name + ", please add one.");
            return; // Exit if no NavMeshAgent found
        }

        // Find and set the target by tag
        GameObject targetGameObject = GameObject.FindGameObjectWithTag("Emily");
        if (targetGameObject != null)
        {
            target = targetGameObject.transform;
        }
        else
        {
            Debug.LogError("Target with tag 'Emily' not found in the scene.");
        }
    }

    void Update()
    {
        if (target != null && agent != null) // Make sure the target and the NavMeshAgent are available
        {
            agent.SetDestination(target.position);
        }
    }
}

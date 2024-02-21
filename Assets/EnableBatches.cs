using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectManager : MonoBehaviour
{
    private List<GameObject> allGameObjects; // List to store all child game objects
    private List<GameObject> activeGameObjects; // List to keep track of currently active game objects

    public int batchSize = 10; // Number of game objects to enable in each batch
    public float activationInterval = 30f; // Time interval between batches

    void Start()
    {
        // Initialize lists
        allGameObjects = new List<GameObject>();
        activeGameObjects = new List<GameObject>();

        // Get all child game objects
        foreach (Transform child in transform)
        {
            allGameObjects.Add(child.gameObject);
        }

        StartCoroutine(EnableRandomGameObjectsRoutine());
    }

    IEnumerator EnableRandomGameObjectsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(activationInterval);
            EnableRandomGameObjects();
        }
    }

    void EnableRandomGameObjects()
    {
        // Disable currently active game objects
        foreach (var obj in activeGameObjects)
        {
            obj.SetActive(false);
        }

        // Clear the list of active game objects
        activeGameObjects.Clear();

        // Enable a batch of random game objects
        for (int i = 0; i < batchSize; i++)
        {
            GameObject randomObject = GetRandomInactiveObject();
            if (randomObject != null)
            {
                randomObject.SetActive(true);
                activeGameObjects.Add(randomObject);
            }
        }
    }

    GameObject GetRandomInactiveObject()
    {
        List<GameObject> inactiveObjects = new List<GameObject>();

        // Find all inactive game objects
        foreach (var obj in allGameObjects)
        {
            if (!obj.activeSelf)
            {
                inactiveObjects.Add(obj);
            }
        }

        // Return a random inactive game object
        return inactiveObjects.Count > 0 ? inactiveObjects[Random.Range(0, inactiveObjects.Count)] : null;
    }
}

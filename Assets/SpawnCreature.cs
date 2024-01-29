using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreature : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // Assign your prefabs in the Unity Inspector
    public GameObject[] spawnPoints; // Assign your spawn points in the Unity Inspector
    public int minSpawnCount = 1; // Minimum number of creatures to spawn each interval
    public int maxSpawnCount = 10; // Maximum number of creatures to spawn each interval

    void Start()
    {
        // Ensure there's at least one prefab to spawn
        if (prefabsToSpawn.Length == 0)
        {
            Debug.LogError("No prefabs assigned in prefabsToSpawn!");
            return;
        }

        // Ensure there's at least one spawn point
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        // Spawn one creature at each spawn point at the start
        foreach (var spawnPoint in spawnPoints)
        {
            SpawnPrefab(spawnPoint);
        }

        // Start the coroutine to spawn creatures at intervals
        StartCoroutine(SpawnCreaturesAtIntervals());
    }

    IEnumerator SpawnCreaturesAtIntervals()
    {
        while (true)
        {
            // Wait for a set interval (e.g., 60 seconds)
            yield return new WaitForSeconds(60);

            // Calculate the number of creatures to spawn
            int creaturesToSpawn = Random.Range(minSpawnCount, Mathf.Min(maxSpawnCount, spawnPoints.Length) + 1);

            // Create a list of available spawn points
            List<GameObject> availableSpawnPoints = new List<GameObject>(spawnPoints);

            for (int i = 0; i < creaturesToSpawn; i++)
            {
                if (availableSpawnPoints.Count == 0)
                {
                    break; // Exit loop if no more spawn points are available
                }

                // Randomly select a spawn point from the available ones
                int spawnPointIndex = Random.Range(0, availableSpawnPoints.Count);
                GameObject spawnPoint = availableSpawnPoints[spawnPointIndex];

                // Remove the selected spawn point from the list of available spawn points
                availableSpawnPoints.RemoveAt(spawnPointIndex);

                // Spawn a creature at the selected spawn point
                SpawnPrefab(spawnPoint);
            }
        }
    }

    void SpawnPrefab(GameObject spawnPoint)
    {
        // Choose a random prefab from the array
        GameObject prefabToSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        Vector3 spawnPosition = spawnPoint.transform.position;

        // Instantiate the prefab at the chosen spawn point
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreature : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // Assign your prefabs in the Unity Inspector
    public float minX, maxX, minZ, maxZ; // Set these in the Unity Inspector

    void Start()
    {
        // Ensure there's at least one prefab to spawn
        if (prefabsToSpawn.Length == 0)
        {
            Debug.LogError("No prefabs assigned in prefabsToSpawn!");
            return;
        }

        StartCoroutine(SpawnCreaturesAtIntervals());
    }

    IEnumerator SpawnCreaturesAtIntervals()
    {
        while (true)
        {
            // Wait for 60 seconds
            yield return new WaitForSeconds(60);

            // Spawn a random number of creatures
            int creaturesToSpawn = Random.Range(1, 21); // Random number between 1 and 20
            for (int i = 0; i < creaturesToSpawn; i++)
            {
                SpawnPrefab();
            }
        }
    }

    void SpawnPrefab()
    {
        // Choose a random prefab from the array
        GameObject prefabToSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];

        // Generate a random position
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 spawnPosition = new Vector3(randomX, 0, randomZ); // Assuming Y is zero or ground level

        // Instantiate the prefab at the generated position
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}

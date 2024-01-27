//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SpawnCube : MonoBehaviour
//{
//    public GameObject prefabToSpawn; // Assign your prefab in the Unity Inspector
//    public float minX, maxX, minZ, maxZ; // Set these in the Unity Inspector

//    void Start()
//    {
//        SpawnPrefab();
//    }

//    void SpawnPrefab()
//    {
//        if (prefabToSpawn == null)
//        {
//            Debug.LogError("Prefab is not assigned!");
//            return;
//        }

//        // Generate a random X and Z position within the specified boundaries
//        float randomX = Random.Range(minX, maxX);
//        float randomZ = Random.Range(minZ, maxZ);

//        // Calculate the Y position as half the height of the prefab
//        float prefabHeight = prefabToSpawn.GetComponent<Renderer>().bounds.size.y;
//        float posY = prefabHeight / 2;

//        // Create a new position vector
//        Vector3 spawnPosition = new Vector3(randomX, posY, randomZ);

//        // Instantiate the prefab at the generated position
//        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
//    }
//}

using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab; // NPC prefab to spawn
    public float spawnRadius = 10f; // Radius around the spawner where NPCs can appear
    public int npcCount = 5; // Number of NPCs to spawn

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < npcCount; i++)
        {
            Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius + transform.position;
            spawnPosition.y = 0; // Adjust based on your ground level or NavMesh
            Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
        }
    }
}

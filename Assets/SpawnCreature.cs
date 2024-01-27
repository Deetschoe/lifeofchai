using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreature : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign your prefab in the Unity Inspector
    public float minX, maxX, minZ, maxZ; // Set these in the Unity Inspector
    public int numberOfPrefabsToSpawn = 1; // Set the number of prefabs to spawn
    
    
    private List<string> creatureNames = new List<string>()
    {
        "Bulbasaur", "Ivysaur", "Venusaur", "Charmander", "Charmeleon",
        "Charizard", "Squirtle", "Wartortle", "Blastoise", "Caterpie",
        "Metapod", "Butterfree", "Weedle", "Kakuna", "Beedrill",
        "Pidgey", "Pidgeotto", "Pidgeot", "Rattata", "Raticate",
        "Spearow", "Fearow", "Ekans", "Arbok", "Pikachu",
        "Raichu", "Sandshrew", "Sandslash", "Nidoran?", "Nidorina",
        "Nidoqueen", "Nidoran?", "Nidorino", "Nidoking", "Clefairy",
        "Clefable", "Vulpix", "Ninetales", "Jigglypuff", "Wigglytuff",
        "Zubat", "Golbat", "Oddish", "Gloom", "Vileplume",
        "Paras", "Parasect", "Venonat", "Venomoth", "Diglett"
    };

    int CalculateHealth(int level)
    {
        if (level == 1) return 50; // Base case for level 1

        int health = 50;
        int increment = 10;

        for (int i = 2; i <= level; i++)
        {
            health += increment;
            increment *= 2; // Double the increment for the next level
        }

        return health;
    }
    void Start()
    {
        for (int i = 0; i < numberOfPrefabsToSpawn; i++)
        {
            SpawnPrefab();
        }
    }

    void SpawnPrefab()
    {
        if (prefabToSpawn == null)
        {
            Debug.LogError("Prefab is not assigned!");
            return;
        }

        // Generate a random X and Z position within the specified boundaries
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        // Calculate the Y position as half the height of the prefab
        float prefabHeight = prefabToSpawn.GetComponent<Renderer>().bounds.size.y;
        float posY = prefabHeight / 2;

        // Create a new position vector
        Vector3 spawnPosition = new Vector3(randomX, posY, randomZ);

        // Instantiate the prefab at the generated position
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        EntityLabelUpdater labelUpdater = spawnedObject.GetComponent<EntityLabelUpdater>();
        if (labelUpdater != null)
        {
            // Randomly select a Pokémon name
            labelUpdater.entityName = creatureNames[Random.Range(0, creatureNames.Count)];

            // Generate a random level with a non-linear distribution
            labelUpdater.entityLevel = GenerateRandomLevel();
            int health = CalculateHealth(labelUpdater.entityLevel);
            HealthBarGradient healthBar = spawnedObject.GetComponent<HealthBarGradient>();
            healthBar.maxHealth = health;
            healthBar.currentHealth = health;

            labelUpdater.UpdateText();
        }
        else
        {
            Debug.LogError("EntityLabelUpdater component not found on the spawned prefab!");
        }
    }

    // Method to generate a random level with larger integers being more rare
    int GenerateRandomLevel()
    {
        float randomNumber = Random.value;
        return Mathf.FloorToInt(Mathf.Pow(randomNumber, 3) * 12) + 1;
    }

}

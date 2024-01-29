using UnityEngine;
using System.Collections; // Required for IEnumerator

public class SetDefaultLocation : MonoBehaviour
{
    public MonoBehaviour BallSpawnCreature; // Assign this in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine
        StartCoroutine(TeleportAndActivate());
    }

    IEnumerator TeleportAndActivate()
    {
        GameObject spawnHereObject = GameObject.FindGameObjectWithTag("spawnHere");

        if (spawnHereObject == null)
        {
            Debug.LogError("No object found with the tag 'spawnHere'");
            yield break;
        }

        float startTime = Time.time;

        // Teleport for the first 5 seconds
        while (Time.time - startTime < 5)
        {
            transform.position = spawnHereObject.transform.position;
            yield return null; // Wait for the next frame
        }

        // After 5 seconds, enable the BallSpawnCreature script
        if (BallSpawnCreature != null)
        {
            BallSpawnCreature.enabled = true;
        }
        else
        {
            Debug.LogError("BallSpawnCreature script not assigned!");
        }
    }
}

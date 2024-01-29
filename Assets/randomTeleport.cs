using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomTeleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TeleportAfterRandomTime());
    }

    IEnumerator TeleportAfterRandomTime()
    {
        // Generate a random time between 15 and 45 seconds
        float waitTime = Random.Range(15f, 45f);

        // Wait for the generated amount of time
        yield return new WaitForSeconds(waitTime);

        // Load the Village scene
        SceneManager.LoadScene("Village");
    }
}

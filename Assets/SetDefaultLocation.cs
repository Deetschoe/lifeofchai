using UnityEngine;

public class SetDefaultLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Find the game object with the tag "spawnHere"
        GameObject spawnHereObject = GameObject.FindGameObjectWithTag("spawnHere");

        if (spawnHereObject != null)
        {
            // Set this object's position to the center of the spawnHere object
            transform.position = spawnHereObject.transform.position;
        }
        else
        {
            Debug.LogError("No object found with the tag 'spawnHere'");
        }
    }
}

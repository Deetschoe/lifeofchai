using UnityEngine;
using System.Collections;

public class EnableAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Disable the game object
        gameObject.SetActive(false);

        // Start the coroutine to enable the game object after 16 seconds
        StartCoroutine(EnableGameObjectAfterTime(3));
    }

    // Coroutine to enable the game object after a specified time
    IEnumerator EnableGameObjectAfterTime(float time)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Enable the game object
        gameObject.SetActive(true);
    }
}

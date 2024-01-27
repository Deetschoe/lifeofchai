using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerPlaneVisibility : MonoBehaviour
{
    public GameObject playerPlaneObject; // Reference to the player's plane object
    public string sceneToLoad;           // Name of the scene to load
    public AudioSource audioSource;      // Reference to the AudioSource component
    public GameObject whiteScreenPanel;  // Reference to the white screen UI panel

    private void Start()
    {
        if (playerPlaneObject != null)
        {
            playerPlaneObject.SetActive(false);
        }
        if (whiteScreenPanel != null)
        {
            whiteScreenPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPlaneObject.SetActive(true);
            whiteScreenPanel.SetActive(true); // Activate the white screen

            if (audioSource != null)
            {
                audioSource.Play(); // Play the audio
            }

            StartCoroutine(ActivatePlaneAndLoadScene());
        }
    }

    private IEnumerator ActivatePlaneAndLoadScene()
    {
        yield return new WaitForSeconds(2.3f);

        playerPlaneObject.SetActive(false);
        whiteScreenPanel.SetActive(false); // Deactivate the white screen

        SceneManager.LoadScene(sceneToLoad);
    }
}

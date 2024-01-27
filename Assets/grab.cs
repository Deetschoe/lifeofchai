using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayAudioOnFirstPickup : MonoBehaviour
{
    public AudioClip pickupAudioClip; // Public variable to assign the first audio clip
    public AudioClip secondAudioClip; // Public variable to assign the second audio clip
    public GameObject targetGameObject; // Public variable to assign the target GameObject
    public GameObject objectA; // The object that will disappear
    public GameObject objectB; // The object that will appear


    public GameObject objectC; // The object that will disappear
    public GameObject objectD; // The object that will appear

    private bool hasBeenPickedUp = false;
    private AudioSource targetAudioSource;

    void Start()
    {
        if (targetGameObject != null)
        {
            targetAudioSource = targetGameObject.GetComponent<AudioSource>();
            if (targetAudioSource != null)
            {
                // Ensure the AudioSource is configured correctly
                targetAudioSource.playOnAwake = false;
                targetAudioSource.loop = false;
            }
        }
    }

    public void OnPickup()
    {
        if (!hasBeenPickedUp && pickupAudioClip != null && targetAudioSource != null)
        {
            targetAudioSource.clip = pickupAudioClip; // Set the first audio clip
            targetAudioSource.Play();
            StartCoroutine(SequenceOfEvents());
            hasBeenPickedUp = true;
        }
    }

    private IEnumerator SequenceOfEvents()
    {
        yield return new WaitForSeconds(3);

        // Make objectA disappear
        if (objectC != null)
        {
            objectC.SetActive(false);
        }
        if (objectD != null)
        {
            objectD.SetActive(true);
        }


        yield return new WaitForSeconds(3);

        // Play the second audio clip
        if (secondAudioClip != null)
        {
            targetAudioSource.clip = secondAudioClip;
            targetAudioSource.Play();
        }

        yield return new WaitForSeconds(1);

        // Make objectA disappear
        if (objectA != null)
        {
            objectA.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);

        // Make objectB appear
        if (objectB != null)
        {
            objectB.SetActive(true);
        }
        yield return new WaitForSeconds(9f);

        SceneManager.LoadScene("School2");

    }
}

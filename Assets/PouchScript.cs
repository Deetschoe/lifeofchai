using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class PouchScript : MonoBehaviour
{
    public float proximityThreshold = 1f;
    private List<Transform> pouchableObjects = new List<Transform>();
    public AudioClip pouchSound; // Reference to the audio clip
    private AudioSource audioSource; // Audio source component

    private float audioCooldown = 1.0f; // Cooldown duration in seconds
    private float lastAudioPlayTime = 0.0f; // Time when the audio was last played

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource component if not already attached
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, proximityThreshold);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("pouchable") && !pouchableObjects.Contains(hitCollider.transform))
            {
                AddPouchableObject(hitCollider.transform);
            }
        }

        // Update positions and remove objects if they are grabbed out of the pouch
        for (int i = pouchableObjects.Count - 1; i >= 0; i--)
        {
            Transform obj = pouchableObjects[i];
            XRGrabInteractable grabbable = obj.GetComponent<XRGrabInteractable>();
            bool isGrabbed = grabbable != null && grabbable.isSelected;

            if (obj == null || (isGrabbed && Vector3.Distance(transform.position, obj.position) > proximityThreshold))
            {
                RemovePouchableObject(obj);
            }
            else
            {
                UpdatePouchableObject(obj);
            }
        }
    }

    private void AddPouchableObject(Transform obj)
    {
        pouchableObjects.Add(obj);
        SetPouchableObjectPhysics(obj, false);
        PlaySound(); // Play sound when object is added
        SetOpacity(obj, 0.9f); // Set lower opacity when added to pouch
    }

    private void RemovePouchableObject(Transform obj)
    {
        if (obj != null)
        {
            SetPouchableObjectPhysics(obj, true);
            pouchableObjects.Remove(obj);
            PlaySound(); // Play sound when object is removed
            SetOpacity(obj, 1f); // Reset to normal opacity when removed from pouch
        }
    }
    private void SetOpacity(Transform obj, float alpha)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Color color = renderer.material.color;
            color.a = alpha;
            renderer.material.color = color;
        }
    }
    private void PlaySound()
    {
        if (pouchSound != null && Time.time - lastAudioPlayTime >= audioCooldown)
        {
            audioSource.PlayOneShot(pouchSound);
            lastAudioPlayTime = Time.time; // Update the time of the last audio play
        }
    }


    private void UpdatePouchableObject(Transform obj)
    {
        if (obj != null)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            rb.velocity = Vector3.zero;
            rb.rotation = Quaternion.identity; // Resetting the rotation

            rb.angularVelocity = Vector3.zero;
            obj.position = transform.position; // or some offset inside the pouch
        }
    }

    private void SetPouchableObjectPhysics(Transform obj, bool useGravity)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            //rb.useGravity = useGravity;
            if (useGravity)
            {
                // Resetting velocity and angular velocity to default when gravity is on
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}

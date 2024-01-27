using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AddComponentsToObjects : MonoBehaviour
{
    public XRGrabInteractable interactablePrefab; // Assign your XR Interactable prefab here.

    private void Start()
    {
        // Find all GameObjects in your scene that need the components.
        GameObject[] objectsToAddComponentsTo = GameObject.FindGameObjectsWithTag("grabbable"); // Use the appropriate tag.

        foreach (GameObject obj in objectsToAddComponentsTo)
        {
            // Add a Rigidbody component if it doesn't already exist.
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = obj.AddComponent<Rigidbody>();
            }

            // Add a BoxCollider component if it doesn't already exist.
            BoxCollider boxCollider = obj.GetComponent<BoxCollider>();
            if (boxCollider == null)
            {
                boxCollider = obj.AddComponent<BoxCollider>();
            }

            // Add an XRGrabInteractable component if it doesn't already exist.
            XRGrabInteractable grabInteractable = obj.GetComponent<XRGrabInteractable>();
            if (grabInteractable == null)
            {
                grabInteractable = obj.AddComponent<XRGrabInteractable>();
            }

            // You can configure the components here as needed.
        }
    }
}

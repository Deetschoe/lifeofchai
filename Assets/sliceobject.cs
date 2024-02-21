using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Material crossSectionMaterial; // Assign in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sword"))
        {
            // Assuming this script is attached to the parent object
            // or the individual parts that you wish to slice.
            GameObject target = this.gameObject;

            // Perform the slice
            Slice(target, other.transform);
        }
    }

    private void Slice(GameObject target, Transform swordTransform)
    {
        if (target.GetComponent<MeshFilter>() != null)
        {
            // Calculate the direction based on the sword's orientation
            Vector3 direction = swordTransform.right; // Use the appropriate direction that matches your sword's slicing direction
            Vector3 position = swordTransform.position; // Position for the slice

            SlicedHull slicedHull = target.Slice(position, direction, crossSectionMaterial);
            if (slicedHull != null)
            {
                GameObject upperHull = slicedHull.CreateUpperHull(target, crossSectionMaterial);
                GameObject lowerHull = slicedHull.CreateLowerHull(target, crossSectionMaterial);

                if (upperHull != null && lowerHull != null)
                {
                    // Setup the sliced parts to spawn correctly and enable physics interactions
                    SetupSlicedObject(upperHull, target.transform);
                    SetupSlicedObject(lowerHull, target.transform);

                    // Optionally, if you want to deactivate the entire parent object including all children
                    if (target.transform.parent != null)
                    {
                        target.transform.parent.gameObject.SetActive(false);
                    }
                    else
                    {
                        target.SetActive(false);
                    }
                }
            }
            else
            {
                Debug.LogWarning("Slice operation failed: Could not slice the target.");
            }
        }
    }

    // Configure the sliced object's position, rotation, and add necessary physics components
    private void SetupSlicedObject(GameObject obj, Transform originalTransform)
    {
        obj.transform.position = originalTransform.position;
        obj.transform.rotation = originalTransform.rotation;
        Rigidbody rb = obj.AddComponent<Rigidbody>();
        MeshCollider collider = obj.AddComponent<MeshCollider>();
        collider.convex = true;
    }
}

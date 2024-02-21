using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Material crossSectionMaterial; // Assign this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sword")) // Ensure your sword GameObject's tag is exactly "sword"
        {
            // This gameObject is the target to be sliced
            GameObject target = gameObject;

            // No need to check for the "Enemy" tag here since the script itself should only be attached to enemies

            // Ensure the target has a MeshFilter component
            if (target.GetComponent<MeshFilter>() != null)
            {
                Slice(target, other.transform);
            }
        }
    }

    private void Slice(GameObject target, Transform swordTransform)
    {
        // Use the sword's transform to determine the slicing direction
        Vector3 direction = swordTransform.right; // Adjust this based on your sword's orientation
        Vector3 position = swordTransform.position; // Position for the slice

        // Perform the slice
        SlicedHull slicedHull = target.Slice(position, direction, crossSectionMaterial);
        if (slicedHull != null)
        {
            GameObject upperHull = slicedHull.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = slicedHull.CreateLowerHull(target, crossSectionMaterial);

            if (upperHull != null && lowerHull != null)
            {
                MakeItPhysical(upperHull);
                MakeItPhysical(lowerHull);

                Destroy(target); // Remove the original object
            }
        }
        else
        {
            Debug.LogWarning("Slice operation failed: Could not slice the target.");
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<Rigidbody>();
        MeshCollider collider = obj.AddComponent<MeshCollider>();
        collider.convex = true; // Ensure this is set for proper physics interaction
    }
}

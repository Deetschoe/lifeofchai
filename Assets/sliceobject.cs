using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Material crossSectionMaterial; // Assign in the Inspector
    public float explodeForce = 5f; // Adjust the force to get the desired effect
    public Vector3 explodeDirectionOffset = Vector3.up; // Adjust this to control the explosion direction

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("sword"))
        {
            GameObject target = this.gameObject;
            Slice(target, other.transform);
        }
    }

    private void Slice(GameObject target, Transform swordTransform)
    {
        if (target.GetComponent<MeshFilter>() != null)
        {
            Vector3 direction = swordTransform.right;
            Vector3 position = swordTransform.position;

            SlicedHull slicedHull = target.Slice(position, direction, crossSectionMaterial);
            if (slicedHull != null)
            {
                GameObject upperHull = slicedHull.CreateUpperHull(target, crossSectionMaterial);
                GameObject lowerHull = slicedHull.CreateLowerHull(target, crossSectionMaterial);

                if (upperHull != null && lowerHull != null)
                {
                    SetupSlicedObject(upperHull, target.transform, direction);
                    SetupSlicedObject(lowerHull, target.transform, -direction);

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

    // Configure the sliced object's position, rotation, add necessary physics components, and apply force
    private void SetupSlicedObject(GameObject obj, Transform originalTransform, Vector3 sliceDirection)
    {
        obj.transform.position = originalTransform.position;
        obj.transform.rotation = originalTransform.rotation;

        Rigidbody rb = obj.AddComponent<Rigidbody>();
        MeshCollider collider = obj.AddComponent<MeshCollider>();
        collider.convex = true;

        // Apply a force to make the sliced parts explode away from each other
        Vector3 forceDirection = (sliceDirection + explodeDirectionOffset).normalized * explodeForce;
        rb.AddForce(forceDirection, ForceMode.Impulse);
    }
}

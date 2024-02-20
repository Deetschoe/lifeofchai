using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class SliceObject : MonoBehaviour
{
    public Transform planeDebug; // Used to determine the slice plane
    public GameObject target; // The GameObject to slice

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && target != null)
        {
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        if (planeDebug == null)
        {
            Debug.LogWarning("Slice operation failed: 'planeDebug' is not assigned.");
            return;
        }

        // Check if the target has a MeshFilter component before attempting to slice
        if (target.GetComponent<MeshFilter>() == null)
        {
            Debug.LogWarning("Slice operation failed: Provided GameObject must have a MeshFilter Component.");
            return;
        }

        SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);
        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, null); // Add a material if you have one
            GameObject lowerHull = hull.CreateLowerHull(target, null); // Add a material if you have one

            if (upperHull != null && lowerHull != null)
            {
                Transform parentTransform = target.transform.parent;
                upperHull.transform.SetParent(parentTransform, false);
                lowerHull.transform.SetParent(parentTransform, false);

                upperHull.transform.localPosition = target.transform.localPosition;
                lowerHull.transform.localPosition = target.transform.localPosition;
                upperHull.transform.localRotation = target.transform.localRotation;
                lowerHull.transform.localRotation = target.transform.localRotation;
                upperHull.transform.localScale = target.transform.localScale;
                lowerHull.transform.localScale = target.transform.localScale;

                // Setting up Rigidbody and MeshCollider for physical interactions
                AddPhysicsComponents(upperHull);
                AddPhysicsComponents(lowerHull);
            }
        }
        else
        {
            Debug.LogWarning("Slice operation failed: Could not slice the target.");
        }
    }

    // Helper method to add physics components to sliced objects
    private void AddPhysicsComponents(GameObject obj)
    {
        var rb = obj.AddComponent<Rigidbody>();
        var collider = obj.AddComponent<MeshCollider>();
        collider.convex = true;
    }
}

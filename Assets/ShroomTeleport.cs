using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HandTeleporter : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;

    private void Update()
    {
        // Check if either hand is close enough to the object
        if (IsHandClose(leftHand) || IsHandClose(rightHand))
        {
            // Load ShroomScene
            SceneManager.LoadScene("ShroomScene");
        }
    }

    private bool IsHandClose(Transform hand)
    {
        if (hand == null) return false;

        float distance = Vector3.Distance(hand.position, transform.position);
        return distance <= 1f; // 1f is the distance threshold
    }
}

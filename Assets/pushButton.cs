using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonTap : MonoBehaviour
{
    private Vector3 initialPosition;
    private float tapThreshold = 0.05f; // Threshold for the tap movement in the X direction
    private XRBaseInteractable interactable;

    void Start()
    {
        interactable = GetComponent<XRBaseInteractable>();
        initialPosition = transform.localPosition;

        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs arg)
    {
        // This function is called when the button is grabbed
    }

    private void OnSelectExited(SelectExitEventArgs arg)
    {
        // Check if the button was tapped in the X direction
        if (Mathf.Abs(transform.localPosition.x - initialPosition.x) > tapThreshold)
        {
            ButtonTapped();
        }

        // Reset the button's position
        transform.localPosition = initialPosition;
    }

    private void ButtonTapped()
    {
        Debug.Log("Button tapped in the X direction!");
        // Add your action here
    }
}

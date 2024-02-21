using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class GrabToChangeScene : MonoBehaviour
{
    public string nextSceneName; // Set this in the Inspector

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("GrabToChangeScene script requires an XRGrabInteractable component on the same GameObject.");
            return;
        }

        grabInteractable.onSelectEntered.AddListener(OnGrabbed);
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.onSelectEntered.RemoveListener(OnGrabbed);
        }
    }

    private void OnGrabbed(XRBaseInteractor interactor)
    {
        SceneManager.LoadScene(nextSceneName);
    }
}

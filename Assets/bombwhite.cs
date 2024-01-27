using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VRTriggerWhiteScreen : MonoBehaviour
{
    public GameObject whitePlaneObject; // 3D Plane object

    private Renderer whitePlaneRenderer;
    private Material whiteMaterial;
    private Color transparentWhite;
    private Color opaqueWhite;

    private void Start()
    {
        if (whitePlaneObject != null)
        {
            whitePlaneRenderer = whitePlaneObject.GetComponent<Renderer>();
            whiteMaterial = whitePlaneRenderer.material;
            transparentWhite = new Color(1f, 1f, 1f, 0f);
            opaqueWhite = new Color(1f, 1f, 1f, 1f);

            // Set initial color to transparent white
            whiteMaterial.color = transparentWhite;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Make the plane white and opaque instantly
            if (whitePlaneObject != null)
            {
                whiteMaterial.color = opaqueWhite;
            }
        }
    }
}
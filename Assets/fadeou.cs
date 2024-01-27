using UnityEngine;
using System.Collections;

public class FadeOutAndDisappear : MonoBehaviour
{
    public float delayBeforeFading = 3f;
    public float fadeDuration = 4f;

    private Renderer objectRenderer;
    private MaterialPropertyBlock propertyBlock;
    private Color originalColor;
    private bool isFading = false;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();

        if (objectRenderer != null && objectRenderer.material != null)
        {
            originalColor = objectRenderer.material.color;
            StartCoroutine(FadeOutRoutine());
        }
        else
        {
            Debug.LogError("Renderer or material not found on the object");
        }
    }

    private IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(delayBeforeFading);

        isFading = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            objectRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor("_Color", newColor);
            objectRenderer.SetPropertyBlock(propertyBlock);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectRenderer.enabled = false;
        OnFadeOutComplete(); // Optional: callback when fade-out is complete
    }

    private void OnFadeOutComplete()
    {
        // Optional: Implement any actions after the fade-out is complete
        // For example: Destroy(gameObject);
    }
}

using UnityEngine;
using TMPro;
using System.Collections;  // This line is added to include the System.Collections namespace

public class FadeOutText : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float fadeOutTime = 3f;

    void Start()
    {
        // Start the FadeOut coroutine
        StartCoroutine(FadeOutTextRoutine());
    }

    private IEnumerator FadeOutTextRoutine()
    {
        // Wait for 3 seconds before starting the fade out
        yield return new WaitForSeconds(3f);

        // Fade out over fadeOutTime seconds
        float startAlpha = textMeshPro.color.a;
        for (float t = 0; t < 1; t += Time.deltaTime / fadeOutTime)
        {
            Color newColor = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, Mathf.Lerp(startAlpha, 0, t));
            textMeshPro.color = newColor;
            yield return null;
        }
    }
}

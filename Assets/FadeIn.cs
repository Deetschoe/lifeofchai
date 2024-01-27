using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public float fadeDistance = 1.0f;
    public float minOpacity = 0.1f;
    public float maxOpacity = 0.25f;

    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void Update()
    {
        float closestDistance = float.MaxValue;
        GameObject[] handObjects = GameObject.FindGameObjectsWithTag("hand");

        foreach (GameObject handObject in handObjects)
        {
            float distance = Vector3.Distance(transform.position, handObject.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
            }
        }

        if (closestDistance < fadeDistance)
        {
            float opacity = Mathf.Lerp(maxOpacity, minOpacity, closestDistance / fadeDistance);
            SetOpacity(opacity);
        }
        else
        {
            SetOpacity(minOpacity);
        }
    }

    void SetOpacity(float opacity)
    {
        Color color = originalColor;
        color.a = opacity;
        rend.material.color = color;
    }
}

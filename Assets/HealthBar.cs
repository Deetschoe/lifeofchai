using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarGradient : MonoBehaviour
{
    public Canvas canvas; // Reference to the Canvas
    public int width = 100; // Width of the health bar
    public int height = 20; // Height of the health bar
    public float maxHealth = 100f;
    public float currentHealth;

    private Image healthBarImage; // Image component within the Canvas
    private Texture2D healthBarTexture;

    void Start()
    {
        // Find the Image component within the Canvas
        healthBarImage = canvas.GetComponentInChildren<Image>();
        if (healthBarImage == null)
        {
            Debug.LogError("No Image component found in the children of the Canvas.");
            return;
        }

        // Create a new Texture2D
        healthBarTexture = new Texture2D(width, height);

        // Apply the texture to the health bar image
        healthBarImage.sprite = Sprite.Create(healthBarTexture, new Rect(0.0f, 0.0f, width, height), new Vector2(0.5f, 0.5f), 100.0f);

        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthRatio = currentHealth / maxHealth;

        // Update the color gradient
        for (int x = 0; x < healthBarTexture.width; x++)
        {
            Color color = x < healthBarTexture.width * healthRatio ? Color.green : Color.black;
            for (int y = 0; y < healthBarTexture.height; y++)
            {
                healthBarTexture.SetPixel(x, y, color);
            }
        }

        // Apply the changes to the texture
        healthBarTexture.Apply();
    }

    // Public method to set health
    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();
    }
}

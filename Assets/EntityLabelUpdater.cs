using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Namespace for TextMeshPro

public class EntityLabelUpdater : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Reference to the TextMeshPro component
    public string entityName; // Variable to store the entity name
    public int entityLevel; // Variable to store the entity level

    public void Start()
    {
        UpdateText();
    }


    // Method to update the text
    public void UpdateText()
    {
        if (textMeshPro != null)
        {
            // Update the text to show the entity name and level
            textMeshPro.text = $"{entityName} ({entityLevel})";
        }
        else
        {
            Debug.LogError("TextMeshPro component not set!");
        }
    }
}

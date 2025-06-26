using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DynamicTextVisibility : MonoBehaviour
{
    public ToggleTextVisibility toggleTextVisibility; // Reference to ToggleTextVisibility script

    public ToggleTextVisibility.ButtonTextPair[] buttonAndTextPairs; // Array of ButtonTextPair to assign in Inspector

    void Start()
    {
        // Ensure the ToggleTextVisibility script is assigned
        if (toggleTextVisibility == null)
        {
            Debug.LogError("ToggleTextVisibility script reference is not set!");
            return;
        }

        // Add all button and text pairs to the ToggleTextVisibility manager
        foreach (var pair in buttonAndTextPairs)
        {
            toggleTextVisibility.AddButtonAndTextPair(pair.button, pair.text);
        }
    }

    void OnDestroy()
    {
        // Remove all button and text pairs from the ToggleTextVisibility manager when destroyed
        foreach (var pair in buttonAndTextPairs)
        {
            toggleTextVisibility.RemoveButtonAndTextPair(pair.button);
        }
    }
}

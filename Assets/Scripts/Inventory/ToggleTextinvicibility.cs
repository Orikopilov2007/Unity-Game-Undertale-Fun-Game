using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleTextVisibility : MonoBehaviour
{
    // List to store pairs of buttons and their associated texts
    private List<ButtonTextPair> buttonTextPairs = new List<ButtonTextPair>();

    // Structure to hold a pair of button and text
    [System.Serializable]
    public struct ButtonTextPair
    {
        public Button button;
        public TextMeshProUGUI text;
    }

    // Method to add a button and its associated text to the list
    public void AddButtonAndTextPair(Button button, TextMeshProUGUI text)
    {
        ButtonTextPair pair = new ButtonTextPair { button = button, text = text };
        buttonTextPairs.Add(pair);
        // Ensure text visibility matches button visibility when adding
        text.gameObject.SetActive(button.gameObject.activeInHierarchy);
    }

    // Method to remove a button and its associated text from the list
    public void RemoveButtonAndTextPair(Button button)
    {
        for (int i = 0; i < buttonTextPairs.Count; i++)
        {
            if (buttonTextPairs[i].button == button)
            {
                buttonTextPairs.RemoveAt(i);
                return;
            }
        }
    }

    void Start()
    {
        // Subscribe to onClick events for all buttons
        foreach (var pair in buttonTextPairs)
        {
            pair.button.onClick.AddListener(() => ToggleTextVisibilityForButton(pair));
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from onClick events when object is destroyed
        foreach (var pair in buttonTextPairs)
        {
            pair.button.onClick.RemoveAllListeners();
        }
    }

    // Method to toggle text visibility for a specific button
    private void ToggleTextVisibilityForButton(ButtonTextPair pair)
    {
        pair.text.gameObject.SetActive(false);
    }
}

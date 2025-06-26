using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonTextVisibility : MonoBehaviour
{
    public TextMeshProUGUI associatedText; // Reference to the text to be toggled

    void Start()
    {
        // Ensure text visibility matches button visibility at start
        associatedText.gameObject.SetActive(true);
    }

    void Update()
    {
        // Handle keyboard input for hiding text only
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            if (IsMouseOverButton())
            {
                ToggleTextVisibility();
            }
        }
    }

    public void ToggleTextVisibility()
    {
        associatedText.gameObject.SetActive(false); // Hide the associated text
    }

    bool IsMouseOverButton()
    {
        // Check if mouse is over the button
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);
        return rectTransform.rect.Contains(localMousePosition);
    }
}

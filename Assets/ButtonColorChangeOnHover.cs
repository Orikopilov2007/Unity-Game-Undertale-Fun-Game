using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChangeOnHover : MonoBehaviour
{
    public Button[] buttonsToChange; // Array of buttons to change color

    void Start()
    {
        foreach (Button button in buttonsToChange)
        {
            // Ensure each button has an interactable transition
            ColorBlock cb = button.colors;
            cb.highlightedColor = Color.yellow; // Set highlighted color to yellow
            button.colors = cb;
        }
    }
}

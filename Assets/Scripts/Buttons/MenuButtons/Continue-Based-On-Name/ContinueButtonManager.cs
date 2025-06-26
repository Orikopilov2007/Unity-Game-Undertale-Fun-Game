using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonManager : MonoBehaviour
{
    public Button continueButton; // Reference to the "Continue" button

    void Start()
    {
        // Retrieve the player's name from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "").ToLower();

        // Disable the "Continue" button if the player's name is one of the custom names
        if (IsCustomName(playerName))
        {
            continueButton.interactable = false;
            continueButton.gameObject.SetActive(false); // Hide the button
        }
        else
        {
            continueButton.interactable = true;
            continueButton.gameObject.SetActive(true); // Show the button
        }
    }

    // Method to check if the player's name is one of the custom names
    bool IsCustomName(string playerName)
    {
        // Define your custom names here
        string[] customNames = { "sans", "papyrus", "toriel", "asriel", "flowey", "asgore", "undyne", "alphys", "frisk", "temmie", "toby", "yoav", "yinon", "ori",""};

        // Check if the player's name matches any of the custom names
        foreach (string name in customNames)
        {
            if (playerName.Equals(name))
            {
                return true;
            }
        }
        return false;
    }
}

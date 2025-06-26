using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
    public TMP_InputField playerNameInput; // Reference to the input field assigned in Unity
    public Button doneButton; // Reference to the done button

    void Start()
    {
        playerNameInput.characterLimit = 8; // Set character limit for the input field

        // Check if PlayerName key exists in PlayerPrefs
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            PlayerPrefs.DeleteKey("PlayerName"); // Remove the PlayerName key from PlayerPrefs
        }

        string playerName = PlayerPrefs.GetString("PlayerName", "");
        playerNameInput.text = playerName; // Set the player's name from PlayerPrefs to the input field

        // Add listener for input field changes
        playerNameInput.onValueChanged.AddListener(OnPlayerNameInputChange);

        // Update the interactability of doneButton initially
        UpdateDoneButtonInteractivity();
    }

    void OnPlayerNameInputChange(string newName)
    {
        UpdateDoneButtonInteractivity();
    }

    void UpdateDoneButtonInteractivity()
    {
        // Check if playerNameInput is assigned and not null or empty
        bool isValidInput = playerNameInput != null && !string.IsNullOrEmpty(playerNameInput.text.Trim());
        doneButton.interactable = isValidInput;
    }

    public void OnDoneButtonClick()
    {
        // Check if playerNameInput is assigned and not null or empty
        if (playerNameInput != null && !string.IsNullOrEmpty(playerNameInput.text.Trim()))
        {
            string playerName = playerNameInput.text.Trim();

            PlayerPrefs.SetString("PlayerName", playerName); // Save the player's name to PlayerPrefs
            PlayerPrefs.Save();

            Debug.Log("Player name saved: " + playerName);

            // Proceed with scene-specific logic here if needed
        }
        else
        {
            Debug.Log("Player name is null or empty. Please enter a valid name.");
            // Optionally, you can display a message to the user or perform some UI feedback
        }
    }
}

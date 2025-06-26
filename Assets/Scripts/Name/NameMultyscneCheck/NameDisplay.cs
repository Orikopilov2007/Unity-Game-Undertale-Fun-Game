using UnityEngine;
using TMPro;

public class NameDisplay : MonoBehaviour
{
    public TMP_Text nameText; // Reference to the TextMeshPro text component to display the player's name

    void Start()
    {
        // Retrieve the player's name from PlayerPrefs
        string playerName = PlayerPrefs.GetString("PlayerName", "");

        // Check if the player's name is not empty
        if (!string.IsNullOrEmpty(playerName))
        {
            // Display the player's name
            nameText.text = "" + playerName;
        }
        else
        {
            // Display a warning if the player's name is empty
            Debug.LogWarning("Player's name is empty!");
        }
    }
}

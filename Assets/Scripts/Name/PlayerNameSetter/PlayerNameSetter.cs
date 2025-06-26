using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerNameSetter : MonoBehaviour
{
    public TMP_InputField playerNameInput; // Reference to the input field
    public Button doneButton; // Reference to the done button
    public GameObject friskPrefab; // Reference to the Frisk prefab

    void Start()
    {
        // Set character limit for the input field
        playerNameInput.characterLimit = 8;

        // Retrieve the player's name from PlayerPrefs and set it to the input field
        string playerName = PlayerPrefs.GetString("PlayerName", "");
        playerNameInput.text = playerName;

        // Add event listener for the done button
        doneButton.onClick.AddListener(OnDoneButtonClick);
    }

    void OnDoneButtonClick()
    {
        // Get the player's name from the input field
        string playerName = playerNameInput.text;

        // Save the player's name to PlayerPrefs
        PlayerPrefs.SetString("PlayerName", playerName);

        // Find the Frisk prefab instance in the scene
        GameObject friskInstance = GameObject.Find("Frisk");

        if (friskInstance == null)
        {
            // Instantiate the Frisk prefab if it doesn't exist
            friskInstance = Instantiate(friskPrefab, Vector3.zero, Quaternion.identity);
            friskInstance.name = "Frisk"; // Ensure the instantiated GameObject is named "Frisk"
        }

        // Set the player's name on the Frisk instance
        Player player = friskInstance.GetComponent<Player>();
        if (player != null)
        {
            player.playerName = playerName;
        }
        else
        {
            Debug.LogError("Player component not found on Frisk!");
        }
    }
}
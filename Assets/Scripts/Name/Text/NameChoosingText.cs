using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class NameChoosing : MonoBehaviour
{
    public AudioSource audioSource;
    public TMP_Text displayText;
    public Button continueButton;
    public Button BackButton;
    public CharacterSettings characterSettings;

    private Dictionary<string, string> customTexts = new Dictionary<string, string>()
    {
        {"sans", "nope"},
        {"papyrus", "THERE CAN ONLY BE ONE NYEAHEHEHE"},
        {"toriel", "You have your own name my child"},
        {"asriel", "Respect the dead!"},
        {"flowey", "I already CHOSE this name"},
        {"asgore", "You shall not!"},
        {"undyne", "Get your own name punk"},
        {"alphys", "I...its.. m..my name"},
        {"frisk", "This name already belongs to someone"},
        {"temmie", "hOy i Am tEmMei"},
        {"toby", "Something about this name seems... ANNOYING."},
        {"yoav", "You won't fit the screen..."},
        {"yinon", "Too religious"},
        {"ori", "You are not powerful enough to wield this name"}
    };

    private string trueName = "chara";
    private string trueText = "DETERMINATION";

    private Color charaTextColor = new Color(180f / 255f, 0f, 0f, 200f / 255f);

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "").ToLower();
        StartCoroutine(DisplayText(playerName));
    }

    IEnumerator DisplayText(string playerName)
    {
        string textToDisplay = "";

        // Handle empty player name scenario
        if (string.IsNullOrEmpty(playerName))
        {
            audioSource.enabled = false;
            textToDisplay = "No turning back now...";

            // Display the text without disappearing
            for (int i = 0; i <= textToDisplay.Length; i++)
            {
                displayText.text = textToDisplay.Substring(0, i);
                yield return new WaitForSeconds(0.05f);
            }

            // Wait for 2 seconds before quitting
            yield return new WaitForSeconds(2f);

            // Quit the application
            SceneManager.LoadScene("Name-The-Fallen-Human");
            yield break; // Exit the coroutine early
        }

        // Change font based on player name
        if (customTexts.ContainsKey(playerName))
        {
            audioSource.enabled = false;
            textToDisplay = customTexts[playerName];
            EnableBackButton(true);
            EnableContinueButton(false);
        }
        else if (playerName == trueName)
        {
            audioSource.enabled = true;
            textToDisplay = trueText;
            displayText.color = charaTextColor; // Set color for "DETERMINATION" text
            EnableBackButton(true);
            EnableContinueButton(true);
        }
        else
        {
            audioSource.enabled = false;
            textToDisplay = "Are you sure?";
            EnableBackButton(true);
            EnableContinueButton(true);
        }

        // Change font based on player name
        if (playerName == "sans")
        {
            displayText.font = characterSettings.sansFont;
        }
        else if (playerName == "papyrus")
        {
            displayText.font = characterSettings.papyrusFont;
        }
        else if (playerName == "chara")
        {
            displayText.font = characterSettings.charaFont;
        }
        // Add more font assignments for other characters as needed

        // Display the text without disappearing
        for (int i = 0; i <= textToDisplay.Length; i++)
        {
            displayText.text = textToDisplay.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void EnableContinueButton(bool enable)
    {
        continueButton.interactable = enable;
        continueButton.gameObject.SetActive(enable);
        if (enable)
        {
            continueButton.GetComponentInChildren<TMP_Text>().text = "Continue";
        }
    }

    private void EnableBackButton(bool enable)
    {
        continueButton.interactable = enable;
        continueButton.gameObject.SetActive(enable);
        if (enable)
        {
            continueButton.GetComponentInChildren<TMP_Text>().text = "Back";
        }
    }

    public void OnContinueButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("Name-The-Fallen-Human");
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemSentences
{
    public string itemName;
    public List<string> useSentences;
    public List<string> infoSentences;
}

public class InventoryManager : MonoBehaviour
{
    // References to UI elements
    public GameObject inventoryPanel;
    public GameObject StatsPanel;
    
    public GameObject textBox;
    public TextMeshProUGUI textBoxText;
    public GameObject infoTextBox;
    public TextMeshProUGUI infoTextBoxText;

    // Settings for text display
    public float textBoxDialogueSpeed = 0.05f;
    public AudioSource textAudioSource;

    // Arrays and lists for items and buttons
    public GameObject[] itemSlots;
    public Button[] actionButtons;

    // List of item sentences
    public List<ItemSentences> itemSentences;

    // Flags and properties
    private int currentItemIndex = 0;
    private int currentButtonIndex = 0;
    private bool isInItemMenu = true;
    private bool itemSelected = false;
    private bool useButtonPressedOnce = false;
    private bool infoButtonPressedOnce = false;

    public bool isInventoryOpen { get; private set; }

    // Flags to manage visibility of text boxes
    private bool textBoxVisible = false;
    private bool infoTextBoxVisible = false;

    // Event for text completion
    public delegate void TextCompletedHandler();
    public event TextCompletedHandler OnTextCompleted;

    void Start()
    {
        CloseInventory();
        textBox.SetActive(false);
        infoTextBox.SetActive(false);
        textBoxText.gameObject.SetActive(false);
        infoTextBoxText.gameObject.SetActive(false);

        StatsPanel.SetActive(false); // Ensure the stats panel is closed at start

        OnTextCompleted += StopTextAudio;
    }



    void Update()
    {
        // Handle opening and closing inventory
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.C))
        {
            if (!isInventoryOpen)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }

        // Handle opening and closing the stats panel
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (StatsPanel.activeSelf)
            {
                CloseStatsPanel();
            }
            else
            {
                OpenStatsPanel();
            }
        }

        // Handle closing text boxes if associated audio source is playing
        if (textBoxVisible && textAudioSource.isPlaying && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)))
        {
            HideUseTextBox();
            StartCoroutine(EnableCharacterMovementAfterDelay());
        }

        if (infoTextBoxVisible && textAudioSource.isPlaying && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)))
        {
            HideInfoTextBox();
            StartCoroutine(EnableCharacterMovementAfterDelay());
        }

        // Handle navigation and actions when inventory is open
        if (isInventoryOpen)
        {
            if (isInItemMenu)
            {
                HandleItemMenuNavigation();
            }
            else
            {
                HandleButtonMenuNavigation();
            }

            HandleCancel();

            // Handle key press for using an item or navigating text boxes
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                if (isInItemMenu)
                {
                    if (!itemSelected)
                    {
                        Debug.Log("No item selected.");
                    }
                    else
                    {
                        if (useButtonPressedOnce)
                        {
                            ShowUseTextBoxWithItemInfo();
                        }
                        else
                        {
                            useButtonPressedOnce = true;
                        }
                    }
                }
                else
                {
                    if (currentButtonIndex == 0)
                    {
                        if (infoButtonPressedOnce)
                        {
                            ShowInfoTextBoxWithItemInfo();
                        }
                        else
                        {
                            infoButtonPressedOnce = true;
                        }
                    }
                    else if (currentButtonIndex == 1)
                    {
                        if (useButtonPressedOnce)
                        {
                            ShowUseTextBoxWithItemInfo();
                        }
                        else
                        {
                            useButtonPressedOnce = true;
                        }
                    }
                    else
                    {
                        Debug.Log("Unhandled action button.");
                    }
                }
            }
        }
    }









    // Coroutine to enable character movement after a delay
    IEnumerator EnableCharacterMovementAfterDelay()
    {
        yield return null; // Ensure coroutine runs in the next frame after hiding text boxes

        EnableCharacterMovement(true); // Enable character movement
        isInventoryOpen = false; // Allow opening the inventory again

        itemSelected = false;
        useButtonPressedOnce = false;
        infoButtonPressedOnce = false;
        DeselectAllItems();
        DeselectAllButtons();
        SelectItem(currentItemIndex);
    }



    // Open inventory method
// Open inventory method
    // Open inventory method
// Open inventory method
// Open inventory method
// Open inventory method
    public void OpenInventory()
    {
        // Hide text boxes and stop their coroutines
        HideUseTextBox();
        HideInfoTextBox();

        // Additional code from your original OpenInventory() method
        EnableCharacterMovement(false);
        inventoryPanel.SetActive(true);
        Time.timeScale = 0f; // Pause game when inventory is open (optional)
        isInventoryOpen = true;

        // Initialize currentItemIndex to "Butterscotch Pie"
        currentItemIndex = -1; // Default to -1 if not found
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null && itemSlots[i].name == "Butterscotch Pie")
            {
                currentItemIndex = i;
                break;
            }
        }

        if (currentItemIndex == -1)
        {
            Debug.LogError("Item 'Butterscotch Pie' not found in itemSlots.");
            // Handle this case as per your game logic (e.g., default to another item)
        }

        isInItemMenu = true; // Ensure item menu is selected
        SelectItem(currentItemIndex);
        itemSelected = true; // Mark an item as selected
        useButtonPressedOnce = false; // Reset these flags
        infoButtonPressedOnce = false; // Reset these flags
        currentButtonIndex = 0; // Select the first button in action menu
        SelectButton(currentButtonIndex); // Update button visual state
        UpdateButtonInteractability(); // Update button interactability based on initial state
    }








    // Close inventory method
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        Time.timeScale = 1f; // Resume game when inventory is closed (optional)
        isInventoryOpen = false;
        itemSelected = false;
        DeselectAllButtons();
        useButtonPressedOnce = false;
        infoButtonPressedOnce = false;

        HideUseTextBox();
        HideInfoTextBox();
        StartCoroutine(EnableCharacterMovementAfterDelay());
    }

    // Handle navigation in item menu
    void HandleItemMenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectPreviousItem();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectNextItem();
        }
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            if (itemSelected)
            {
                isInItemMenu = false;
                currentButtonIndex = 0;
                DeselectAllItems();
                SelectButton(currentButtonIndex);
            }
            else
            {
                Debug.Log("No item selected.");
            }
        }
    }
    public void OpenStatsPanel()
    {
        StatsPanel.SetActive(true);
        EnableCharacterMovement(false); // Disable character movement
    }


    public void CloseStatsPanel()
    {
        StatsPanel.SetActive(false);
        EnableCharacterMovement(true); // Enable character movement only if inventory is closed
    }



    // Handle navigation in button menu
    void HandleButtonMenuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectPreviousButton();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectNextButton();
        }
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            HandleButtonClickByKeyboard();
        }
    }

    // Select next item in inventory
    void SelectNextItem()
    {
        int attempts = 0;
        do
        {
            currentItemIndex = (currentItemIndex + 1) % itemSlots.Length;
            attempts++;
        } while (itemSlots[currentItemIndex] == null && attempts < itemSlots.Length);

        UpdateItemSelection();
        itemSelected = true;
        useButtonPressedOnce = false;
        infoButtonPressedOnce = false;
        UpdateButtonInteractability();
    }

    // Select previous item in inventory
    void SelectPreviousItem()
    {
        int attempts = 0;
        do
        {
            currentItemIndex--;
            if (currentItemIndex < 0)
            {
                currentItemIndex = itemSlots.Length - 1;
            }
            attempts++;
        } while (itemSlots[currentItemIndex] == null && attempts < itemSlots.Length);

        UpdateItemSelection();
        itemSelected = true;
        useButtonPressedOnce = false;
        infoButtonPressedOnce = false;
        UpdateButtonInteractability();
    }

    // Select next button in action menu
    void SelectNextButton()
    {
        int previousButtonIndex = currentButtonIndex;
        currentButtonIndex = (currentButtonIndex + 1) % actionButtons.Length;
        UpdateButtonSelection(previousButtonIndex, currentButtonIndex);
    }

    // Select previous button in action menu
    void SelectPreviousButton()
    {
        int previousButtonIndex = currentButtonIndex;
        currentButtonIndex--;
        if (currentButtonIndex < 0)
        {
            currentButtonIndex = actionButtons.Length - 1;
        }
        UpdateButtonSelection(previousButtonIndex, currentButtonIndex);
    }

    // Update visual selection of item in inventory
    void UpdateItemSelection()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null)
            {
                if (i == currentItemIndex)
                {
                    itemSlots[i].GetComponent<InventoryItem>().Select();
                }
                else
                {
                    itemSlots[i].GetComponent<InventoryItem>().Deselect();
                }
            }
        }
    }

    // Update visual selection of button in action menu
    void UpdateButtonSelection(int previousButtonIndex, int currentButtonIndex)
    {
        for (int i = 0; i < actionButtons.Length; i++)
        {
            Button button = actionButtons[i];
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText == null)
            {
                Debug.LogError($"TextMeshProUGUI component not found on button {button.gameObject.name}.");
                continue;
            }

            if (i == currentButtonIndex)
            {
                buttonText.color = Color.yellow;
            }
            else
            {
                buttonText.color = Color.white;
            }
        }
    }

    // Handle button click by keyboard input
    void HandleButtonClickByKeyboard()
    {
        Button selectedButton = actionButtons[currentButtonIndex];
        if (selectedButton != null)
        {
            if (currentButtonIndex == 0)
            {
                if (infoButtonPressedOnce)
                {
                    ShowInfoTextBoxWithItemInfo();
                }
                else
                {
                    infoButtonPressedOnce = true;
                }
            }
            else if (currentButtonIndex == 1)
            {
                if (useButtonPressedOnce)
                {
                    ShowUseTextBoxWithItemInfo();
                }
                else
                {
                    useButtonPressedOnce = true;
                }
            }
            else
            {
                Debug.Log($"Button {selectedButton.gameObject.name} pressed by keyboard.");
            }
        }
        else
        {
            Debug.LogWarning("Selected button is null.");
        }
    }

    // Handle cancel action (close inventory or switch to item menu)
    void HandleCancel()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (!isInItemMenu)
            {
                isInItemMenu = true;
                DeselectAllButtons();
                SelectItem(currentItemIndex);
                itemSelected = true;
                UpdateButtonInteractability();
                useButtonPressedOnce = false;
                infoButtonPressedOnce = false;
            }
            else
            {
                CloseInventory();
            }
        }
    }

    // Show Use text box with item information
// Show Use text box with item information
// Show Use text box with item information
// Show Use text box with item information
    void ShowUseTextBoxWithItemInfo()
    {
        CloseInventory();

        // Immediately clear existing text
        textBoxText.text = "";

        textBox.SetActive(true);
        textBoxText.gameObject.SetActive(true);

        ItemSentences item = FindItemByName(GetItemName(currentItemIndex));

        StartCoroutine(TypeText(item.useSentences));
    }

    void ShowInfoTextBoxWithItemInfo()
    {
        CloseInventory();

        // Immediately clear existing text
        infoTextBoxText.text = "";

        infoTextBox.SetActive(true);
        infoTextBoxText.gameObject.SetActive(true);

        ItemSentences item = FindItemByName(GetItemName(currentItemIndex));

        if (item != null && item.infoSentences != null)
        {
            StartCoroutine(TypeText(item.infoSentences));
        }
        else
        {
            Debug.LogWarning("Item or info sentences not found.");
            HideInfoTextBox();
            StartCoroutine(EnableCharacterMovementAfterDelay());
        }
    }





    // Coroutine to type out text gradually
    // Coroutine to type out text gradually and wait for input to advance
// Coroutine to type out text gradually and wait for input to advance
// Coroutine to type out text gradually and wait for input to advance
// Coroutine to type out text gradually and wait for input to advance
// Coroutine to type out text gradually and wait for input to advance
// Coroutine to type out text gradually and wait for input to advance
// Coroutine to type out text gradually and wait for input to advance
// Coroutine to type out text gradually and wait for input to advance
// Coroutine to type out text gradually and wait for input to advance
// Coroutine to type out text gradually and wait for input to advance
// Coroutine to type out text gradually and wait for input to advance
    IEnumerator TypeText(List<string> sentences)
    {
        // Immediately clear text
        textBoxText.text = "";

        // Track the current sentence index
        int currentSentenceIndex = 0;

        // Type out each sentence sequentially
        while (currentSentenceIndex < sentences.Count)
        {
            string currentSentence = sentences[currentSentenceIndex];
            string displaySentence = "";

            // Restart audio playback at the beginning of each sentence
            if (textAudioSource != null)
            {
                textAudioSource.Stop();
                textAudioSource.Play();
            }

            // Type out the sentence character by character
            for (int i = 0; i < currentSentence.Length; i++)
            {
                displaySentence += currentSentence[i];
                textBoxText.text = displaySentence; // Update text as it types out
                yield return new WaitForSeconds(textBoxDialogueSpeed); // Wait for a small delay per character

                // Check for user input to advance to the next sentence
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
                {
                    break; // Exit the typing loop if user presses Z or Enter
                }
            }

            // Wait for a short delay after each sentence is fully displayed
            yield return new WaitForSeconds(0.1f);

            // Ensure audio playback stops after the sentence is fully displayed
            if (textAudioSource != null && textAudioSource.isPlaying)
            {
                textAudioSource.Stop();
            }

            textBoxText.text += "\n"; // Add a new line after each sentence

            // Wait for "Z" or "Enter" key press before moving to the next sentence
            bool sentenceComplete = false;
            while (!sentenceComplete)
            {
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
                {
                    sentenceComplete = true; // Allow moving to the next sentence
                }
                yield return null; // Wait until the next frame
            }

            currentSentenceIndex++; // Move to the next sentence
        }

        // Text completion: hide text box and invoke text completed event
        textBoxVisible = false;
        textBox.SetActive(false);
        textBoxText.gameObject.SetActive(false);
        OnTextCompleted?.Invoke();
    }







    // Hide Use text box
    void HideUseTextBox()
    {
        textBox.SetActive(false);
        textBoxText.gameObject.SetActive(false);
        textBoxVisible = false;
    }

    // Hide Info text box
    void HideInfoTextBox()
    {
        infoTextBox.SetActive(false);
        infoTextBoxText.gameObject.SetActive(false);
        infoTextBoxVisible = false;
    }

    // Stop text audio when text display is complete
    void StopTextAudio()
    {
        if (textAudioSource != null && textAudioSource.isPlaying)
        {
            textAudioSource.Stop();
        }
    }

    // Deselect all items in inventory
    void DeselectAllItems()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] != null)
            {
                itemSlots[i].GetComponent<InventoryItem>().Deselect();
            }
        }
    }

    // Deselect all buttons in action menu
    void DeselectAllButtons()
    {
        for (int i = 0; i < actionButtons.Length; i++)
        {
            Button button = actionButtons[i];
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText == null)
            {
                Debug.LogError($"TextMeshProUGUI component not found on button {button.gameObject.name}.");
                continue;
            }

            buttonText.color = Color.white;
        }
    }

    // Update button interactability based on item selection
// Update button interactability based on item selection
// Update button interactability based on item selection
// Update button interactability based on item selection
// Update button interactability based on item selection and button press state
    void UpdateButtonInteractability()
    {
        for (int i = 0; i < actionButtons.Length; i++)
        {
            Button button = actionButtons[i];

            if (button != null)
            {
                if (i == 0)
                {
                    button.interactable = true; // Info button is always interactable
                }
                else if (i == 1)
                {
                    button.interactable = itemSelected; // Use button is interactable only if an item is selected
                }
                else if (i >= 2)
                {
                    button.interactable = true; // Enable other buttons as needed
                }

                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText == null)
                {
                    Debug.LogError($"TextMeshProUGUI component not found on button {button.gameObject.name}.");
                    continue;
                }

                // Set text color based on interactability, current button index, and useButtonPressedOnce flag
                if (button.interactable && i == 1 && useButtonPressedOnce)
                {
                    buttonText.color = Color.yellow; // Highlight the "use" button if it's interactable and useButtonPressedOnce is true
                }
                else
                {
                    buttonText.color = Color.white; // Reset color for other buttons or if conditions are not met
                }
            }
        }
    }





    // Get current item index
    public int GetCurrentItemIndex()
    {
        return currentItemIndex;
    }

    // Get current button index
    public int GetCurrentButtonIndex()
    {
        return currentButtonIndex;
    }

    // Select item by index
    public void SelectItem(int index)
    {
        currentItemIndex = index;
        UpdateItemSelection();
    }

    // Select button by index
    // Select button by index
// Select button by index
public void SelectButton(int index)
{
    currentButtonIndex = index;
    UpdateButtonInteractability(); // Update button interactability based on the new selection
    UpdateButtonSelection(currentButtonIndex, currentButtonIndex); // Update button visual state
}



    // Enable or disable character movement
    public void EnableCharacterMovement(bool enable)
    {
        bool canMove = enable && !isInventoryOpen && !StatsPanel.activeSelf;
        // Assuming there's a method to enable/disable character movement
        // Replace `CharacterController` with your actual character controller script
        CharacterController characterController = FindObjectOfType<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = canMove;
        }
    }



    // Get the name of the current item
    string GetItemName(int index)
    {
        if (index >= 0 && index < itemSlots.Length && itemSlots[index] != null)
        {
            return itemSlots[index].name; // Assuming itemSlots[i].name contains the item name
        }
        return "Unknown";
    }

    // Find item by name in itemSentences list
    ItemSentences FindItemByName(string name)
    {
        return itemSentences.Find(item => item.itemName == name);
    }
}

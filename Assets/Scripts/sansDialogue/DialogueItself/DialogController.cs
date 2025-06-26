using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI DialogueText;
    public string[] sentences;
    private int index = 0;
    [SerializeField] public float DialogueSpeed;

    public SpriteRenderer characterSpriteRenderer;
    public Sprite originalSprite;
    public Sprite newSprite;
    public int changeSpriteLine = 2;  // Line at which to change the sprite
    public int revertSpriteLine = 5;  // Line at which to revert to the original sprite

    public AudioSource dialogueSound; // Reference to the AudioSource component

    private bool isDisplayingText = false; // Flag to track if text is currently being displayed
    private bool skipCurrentSentence = false; // Flag to skip the current sentence

    // Start is called before the first frame update
    void Start()
    {
        DisplayNextSentence();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDisplayingText && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)))
        {
            DisplayNextSentence();
        }

        // Check for the keys to skip the current sentence only if the sentence does not contain uppercase letters
        if (isDisplayingText && !ContainsUpperCase(sentences[index - 1]) && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            skipCurrentSentence = true;
        }
    }

    void DisplayNextSentence()
    {
        if (index >= sentences.Length)
        {
            return;
        }

        isDisplayingText = true;

        StartCoroutine(DisplaySentence(sentences[index]));
        index++;

        // Check if the current line of dialogue matches the line to change the sprite
        if (index == changeSpriteLine)
        {
            ChangeSprite(newSprite);
        }
        // Check if the current line of dialogue matches the line to revert the sprite
        else if (index == revertSpriteLine)
        {
            ChangeSprite(originalSprite);
        }
    }

    IEnumerator DisplaySentence(string sentence)
    {
        DialogueText.text = "";

        // Determine the delay based on the presence of uppercase letters in the sentence
        float delay = ContainsUpperCase(sentence) && DialogueSpeed == 0.05f ? DialogueSpeed * 3 : DialogueSpeed;

        // Play the audio only if the sentence is not "..." and does not contain uppercase letters
        if (sentence != "..." && !ContainsUpperCase(sentence))
        {
            dialogueSound.Play(); // Start playing the audio
            dialogueSound.loop = true; // Set the audio to loop
        }

        foreach (char character in sentence.ToCharArray())
        {
            if (skipCurrentSentence)
            {
                DialogueText.text = sentence;
                break;
            }
            DialogueText.text += character;
            yield return new WaitForSeconds(delay);
        }

        isDisplayingText = false;
        skipCurrentSentence = false; // Reset the flag for the next sentence

        if (sentence != "..." && !ContainsUpperCase(sentence))
        {
            dialogueSound.loop = false; // Disable audio looping
        }
    }

    bool ContainsUpperCase(string sentence)
    {
        foreach (char c in sentence)
        {
            if (char.IsUpper(c))
            {
                return true;
            }
        }
        return false;
    }

    void ChangeSprite(Sprite sprite)
    {
        if (characterSpriteRenderer != null && sprite != null)
        {
            characterSpriteRenderer.sprite = sprite;
        }
        else
        {
            Debug.LogError("Character sprite renderer or sprite is not assigned!");
        }
    }
}

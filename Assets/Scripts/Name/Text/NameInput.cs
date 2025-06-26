using UnityEngine;
using TMPro;

public class NameInput : MonoBehaviour
{
    public TMP_InputField nameInputField;

    public void SaveName()
    {
        string playerName = nameInputField.text.Trim().ToLower();
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }
}

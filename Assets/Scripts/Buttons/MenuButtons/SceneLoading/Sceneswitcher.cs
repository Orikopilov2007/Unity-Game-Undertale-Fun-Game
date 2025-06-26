using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private List<ButtonScenePair> buttonScenePairs;

    public List<ButtonScenePair> ButtonScenePairs => buttonScenePairs;

    void Start()
    {
        // Add click listeners for each button in the list
        foreach (var pair in buttonScenePairs)
        {
            if (pair.button != null)
            {
                pair.button.onClick.AddListener(() => OnButtonClick(pair.sceneName));
            }
            else
            {
                Debug.LogError("A button is not assigned.");
            }
        }
    }

    public void OnButtonClick(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not assigned.");
        }
    }
}

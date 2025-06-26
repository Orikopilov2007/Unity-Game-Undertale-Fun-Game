using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TeleportSceneSwitcher : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.8 seconds
        SceneManager.LoadScene("GenocideLastCorridor2");
    }
}
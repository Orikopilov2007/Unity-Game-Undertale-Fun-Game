using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void OnExitButtonClick()
    {
        // Exit the application
        Debug.Log("Exiting application...");
        Application.Quit();
    }
}

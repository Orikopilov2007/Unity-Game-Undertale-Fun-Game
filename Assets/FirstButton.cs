using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnContinueClick()
    {
        SceneManager.LoadScene("Name-The-Fallen-Human");
    }
}

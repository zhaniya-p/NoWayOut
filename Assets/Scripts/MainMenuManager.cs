using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void OpenSettings()
    {
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
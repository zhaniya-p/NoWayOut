using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject keybindPanel;
    public GameObject levelChoicePanel;

    void Start()
    {
        ShowMain();
    }

    void DisableAllPanels()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        keybindPanel.SetActive(false);
        levelChoicePanel.SetActive(false);
    }

    public void ShowMain()
    {
        DisableAllPanels();
        mainPanel.SetActive(true);
    }

    public void ShowSettings()
    {
        DisableAllPanels();
        settingsPanel.SetActive(true);
    }

    public void ShowKeybind()
    {
        DisableAllPanels();
        keybindPanel.SetActive(true);
    }

    public void ShowLevelChoice()
    {
        DisableAllPanels();
        levelChoicePanel.SetActive(true);
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void StartLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
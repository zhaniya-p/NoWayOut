using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class PauseManager : MonoBehaviour
{
    [Header("Pause UI")]
    public GameObject pauseCanvas;
    public GameObject pausePanel;
    public GameObject settingsPanel;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    private bool isPaused = false;
    private bool isRestarting = false;

    void Start()
    {
        pauseCanvas.SetActive(false);

        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (!isRestarting &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        pauseCanvas.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;

        Cursor.visible = isPaused;

        Cursor.lockState = isPaused
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        if (!isPaused)
        {
            ShowPausePanel();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;

        pauseCanvas.SetActive(false);

        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ShowPausePanel()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void RestartScene()
    {
        if (!isRestarting)
        {
            pauseCanvas.SetActive(false);
            StartCoroutine(FadeAndRestart());
        }
    }

    public void ReturnToMenu(string menuSceneName)
    {
        if (!isRestarting)
        {
            pauseCanvas.SetActive(false);
            StartCoroutine(FadeAndLoadMenu(menuSceneName));
        }
    }

    IEnumerator FadeAndRestart()
    {
        isRestarting = true;

        Time.timeScale = 1f;

        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            fadeImage.color =
                new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }

        fadeImage.color =
            new Color(color.r, color.g, color.b, 1f);

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FadeAndLoadMenu(string sceneName)
    {
        isRestarting = true;

        Time.timeScale = 1f;

        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            fadeImage.color =
                new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }

        fadeImage.color =
            new Color(color.r, color.g, color.b, 1f);

        SceneManager.LoadScene(sceneName);
    }
}
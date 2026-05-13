using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float timeLeft = 180f;
    public int totalTasks = 3;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI tasksText;
    public GameObject gameOverCanvas;
    public GameObject winCanvas;
    private int completedTasks = 0;
    private bool gameEnded = false;

    void Awake() { Instance = this; }

    void Update()
    {
        if (gameEnded) return;
        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timeLeft) + "s";
        if (timeLeft <= 0) TriggerLose();
    }

    public void TaskCompleted()
    {
        completedTasks++;
        tasksText.text = completedTasks + "/" + totalTasks;
        if (completedTasks >= totalTasks) TriggerWin();
    }

    public void TriggerLose()
    {
        gameEnded = true;
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
    }

    public void TriggerWin()
    {
        gameEnded = true;
        Time.timeScale = 0;
        winCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
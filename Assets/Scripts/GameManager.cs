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
    public TextMeshProUGUI winScoreText;
    public TextMeshProUGUI loseScoreText;
    private int completedTasks = 0;
    private bool gameEnded = false;
    private int score = 0;

    void Awake() { Instance = this; }

    void Update()
    {
        if (gameEnded) return;
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.CeilToInt(timeLeft) + "s";
        if (timeLeft <= 0) TriggerLose();
    }

    public void UpdateTaskCount(int count)
    {
        tasksText.text = count + "/" + totalTasks;
    }

    public void TaskCompleted()
    {
        completedTasks++;
        score += 100;

        if (completedTasks >= totalTasks)
        {
            completedTasks = 0;
            tasksText.text = "0/" + totalTasks;
            TaskManager.Instance.TaskGroupCompleted();
        }
    }

    public void CallTaskCompleted()
    {
        TaskCompleted();
    }

    public void TriggerLose()
    {
        if (gameEnded) return;
        gameEnded = true;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            player.GetComponent<PlayerController>()?.TriggerLoseAnimation();

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
            PlayerPrefs.SetInt("HighScore", score);

        if (loseScoreText != null)
            loseScoreText.text = "Score: " + score +
                "\nHigh Score: " + PlayerPrefs.GetInt("HighScore", 0);

        StartCoroutine(ShowLoseScreen());
    }

    System.Collections.IEnumerator ShowLoseScreen()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverCanvas.SetActive(true);
    }

    public void TriggerWin()
    {
        if (gameEnded) return;
        gameEnded = true;

        score += Mathf.CeilToInt(timeLeft) * 2;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            player.GetComponent<PlayerController>()?.TriggerWinAnimation();

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
            PlayerPrefs.SetInt("HighScore", score);

        if (winScoreText != null)
            winScoreText.text = "Score: " + score +
                "\nHigh Score: " + PlayerPrefs.GetInt("HighScore", 0);

        StartCoroutine(ShowWinScreen());
    }

    System.Collections.IEnumerator ShowWinScreen()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
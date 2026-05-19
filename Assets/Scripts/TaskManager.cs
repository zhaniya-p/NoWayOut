using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TaskManager : MonoBehaviour
{
    public int[] taskCounts = { 3, 3, 1 };

    [Header("Task Intro Panel")]
    public GameObject taskIntroPanel;
    public TextMeshProUGUI taskTitleText;
    public TextMeshProUGUI taskDescriptionText;
    public Button startButton;

    [Header("Tasks")]
    public string[] taskTitles = {
        "Task 1: Find the Parts",
        "Task 2: Pull the Levers",
        "Task 3: Solve the Puzzle"
    };
    public string[] taskDescriptions = {
        "Find 3 generator parts scattered around the cave.\nPress E to collect.",
        "Activate 3 levers to open the door.\nPress E to interact.",
        "Solve the puzzle — press the buttons in the correct order.\nCode: 1 - 3 - 2 - 4"
    };

    private int currentTask = 0;
    public static TaskManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ShowTaskIntro(0);

        StartCoroutine(ForceShowCursor());
    }

    System.Collections.IEnumerator ForceShowCursor()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowTaskIntro(int taskIndex)
    {
        currentTask = taskIndex;
        taskIntroPanel.SetActive(true);
        taskTitleText.text = taskTitles[taskIndex];
        taskDescriptionText.text = taskDescriptions[taskIndex];
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameManager.Instance.totalTasks = taskCounts[taskIndex];
        GameManager.Instance.tasksText.text = "0/" + taskCounts[taskIndex];
    }

    public void OnStartButtonClicked()
    {
        taskIntroPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TaskGroupCompleted()
    {
        int nextTask = currentTask + 1;
        if (nextTask < taskTitles.Length)
        {
            ShowTaskIntro(nextTask);
        }
        else
        {
            DoorController.Instance.OpenDoor();
        }
    }
}
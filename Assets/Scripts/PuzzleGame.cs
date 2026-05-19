using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PuzzleGame : MonoBehaviour
{
    public AudioClip buttonSound;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    private bool playerNearby = false;
    private bool puzzleActive = false;
    private bool puzzleSolved = false;

    public GameObject hintText;
    public GameObject puzzleUI;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI riddleText;
    public TextMeshProUGUI inputDisplay;

    private int[] correctOrder = { 2, 3, 6, 4 };
    private int[] playerInput = new int[4];
    private int inputIndex = 0;

    void Update()
    {
        if (playerNearby && !puzzleActive && !puzzleSolved)
        {
            if (hintText != null) hintText.SetActive(true);
            if (Keyboard.current.eKey.wasPressedThisFrame)
                OpenPuzzle();
        }
        else
        {
            if (hintText != null) hintText.SetActive(false);
        }
    }

    void OpenPuzzle()
    {
        puzzleActive = true;
        inputIndex = 0;
        playerInput = new int[4];

        if (puzzleUI != null) puzzleUI.SetActive(true);

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (riddleText != null)
            riddleText.text =
                "CAVE TERMINAL\n" +
                "To open the exit door,\n" +
                "enter the correct sequence:\n" +
                "- The smallest prime number\n" +
                "- Sides of a triangle\n" +
                "- Legs of an insect\n" +
                "- Seasons in a year\n";

        if (feedbackText != null)
            feedbackText.text = "";

        if (inputDisplay != null)
            inputDisplay.text = "[ _ ] [ _ ] [ _ ] [ _ ]";
    }

    public void PressButton(int number)
    {
        if (!puzzleActive || puzzleSolved) return;

        if (buttonSound != null)
            AudioSource.PlayClipAtPoint(buttonSound, transform.position);

        playerInput[inputIndex] = number;
        inputIndex++;

        string display = "";
        for (int i = 0; i < 4; i++)
        {
            if (i < inputIndex)
                display += "[ " + playerInput[i] + " ] ";
            else
                display += "[ _ ] ";
        }
        if (inputDisplay != null)
            inputDisplay.text = display;

        if (inputIndex >= 4)
            CheckSolution();
    }

    void CheckSolution()
    {
        bool correct = true;
        for (int i = 0; i < correctOrder.Length; i++)
        {
            if (playerInput[i] != correctOrder[i])
            {
                correct = false;
                break;
            }
        }

        if (correct)
        {
            if (correctSound != null)
                AudioSource.PlayClipAtPoint(correctSound, transform.position);
            if (feedbackText != null)
                feedbackText.text = "CORRECT! Door unlocking...";
            puzzleSolved = true;
            puzzleActive = false;
            StartCoroutine(CompleteWithDelay());
        }
        else
        {
            if (wrongSound != null)
                AudioSource.PlayClipAtPoint(wrongSound, transform.position);
            if (feedbackText != null)
                feedbackText.text = "✗ Wrong sequence! Try again.";
            inputIndex = 0;
            playerInput = new int[4];
            if (inputDisplay != null)
                inputDisplay.text = "[ _ ] [ _ ] [ _ ] [ _ ]";
        }
    }

    System.Collections.IEnumerator CompleteWithDelay()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        ClosePuzzleAndComplete();
    }

    void ClosePuzzleAndComplete()
    {
        if (puzzleUI != null) puzzleUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.Instance.TaskCompleted();
    }

    public void ClosePuzzle()
    {
        if (puzzleSolved) return;
        puzzleActive = false;
        inputIndex = 0;
        playerInput = new int[4];
        if (puzzleUI != null) puzzleUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerNearby = false;
    }
}
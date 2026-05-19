using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    private bool playerNearby = false;
    private bool done = false;
    public GameObject hintText;

    void Update()
    {
        if (playerNearby && !done)
        {
            if (hintText != null) hintText.SetActive(true);

            if (Keyboard.current.eKey.wasPressedThisFrame)
                CompleteTask();
        }
        else
        {
            if (hintText != null) hintText.SetActive(false);
        }
    }

    private static int collectedCount = 0;

    void CompleteTask()
    {
        done = true;
        collectedCount++;
        GameManager.Instance.UpdateTaskCount(collectedCount);
        GameManager.Instance.TaskCompleted();

        if (collectedCount >= 3)
            collectedCount = 0;

        if (hintText != null) hintText.SetActive(false);
        gameObject.SetActive(false);
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
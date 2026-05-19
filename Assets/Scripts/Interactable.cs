using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public GameObject hintText;
    public AudioClip pickupSound;

    private bool playerNearby = false;
    private bool done = false;
    private AudioSource audioSource;
    private static int collectedCount = 0;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

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

    void CompleteTask()
    {
        done = true;
        collectedCount++;

        if (pickupSound != null)
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

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
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool playerNearby = false;
    private bool done = false;

    void Update()
    {
        if (playerNearby && !done && Input.GetKeyDown(KeyCode.E))
        {
            done = true;
            GameManager.Instance.TaskCompleted();
            gameObject.SetActive(false);
        }
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
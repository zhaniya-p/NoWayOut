using UnityEngine;
using UnityEngine.InputSystem;

public class Lever : MonoBehaviour
{
    private bool playerNearby = false;
    private bool activated = false;
    public GameObject hintText;
    public Material activatedMaterial;
    public AudioClip leverSound;
    public static int activatedCount = 0;
    public static int totalLevers = 3;

    void Update()
    {
        if (playerNearby && !activated)
        {
            if (hintText != null) hintText.SetActive(true);

            if (Keyboard.current.eKey.wasPressedThisFrame)
                ActivateLever();
        }
        else
        {
            if (hintText != null) hintText.SetActive(false);
        }
    }

    void ActivateLever()
    {
        activated = true;
        activatedCount++;

        if (leverSound != null)
            AudioSource.PlayClipAtPoint(leverSound, transform.position);

        if (activatedMaterial != null)
            GetComponent<Renderer>().material = activatedMaterial;

        if (hintText != null) hintText.SetActive(false);

        GameManager.Instance.UpdateTaskCount(activatedCount);

        if (activatedCount >= totalLevers)
        {
            activatedCount = 0;
            TaskManager.Instance.TaskGroupCompleted();
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
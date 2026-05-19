using UnityEngine;
using UnityEngine.InputSystem;

public class FootstepSound : MonoBehaviour
{
    public AudioClip walkSound;
    public AudioClip runSound;
    private AudioSource audioSource;
    private float stepTimer = 0f;
    public float walkStepInterval = 0.5f;
    public float runStepInterval = 0.3f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        bool isMoving = keyboard.wKey.isPressed || keyboard.aKey.isPressed ||
                        keyboard.sKey.isPressed || keyboard.dKey.isPressed;
        bool isRunning = keyboard.leftShiftKey.isPressed;

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;
            float interval = isRunning ? runStepInterval : walkStepInterval;

            if (stepTimer <= 0f)
            {
                AudioClip clip = isRunning ? runSound : walkSound;
                if (clip != null)
                    audioSource.PlayOneShot(clip, isRunning ? 1f : 0.7f);
                stepTimer = interval;
            }
        }
    }
}
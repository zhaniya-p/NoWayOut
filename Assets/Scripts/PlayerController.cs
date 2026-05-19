using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;

    [Header("Stamina")]
    public float maxStamina = 100f;
    public float staminaDrain = 20f;
    public float staminaRegen = 10f;
    public Slider staminaBar;

    [Header("Camera")]
    public Transform cameraTransform;

    private Animator animator;
    private CharacterController controller;
    private float verticalVelocity;
    private float stamina;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        stamina = maxStamina;

        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = maxStamina;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleStamina();
        HandleAnimations();
    }

    void HandleMovement()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float h = 0f, v = 0f;
        if (keyboard.aKey.isPressed) h = -1f;
        if (keyboard.dKey.isPressed) h = 1f;
        if (keyboard.sKey.isPressed) v = -1f;
        if (keyboard.wKey.isPressed) v = 1f;

        bool isSprinting = keyboard.leftShiftKey.isPressed && stamina > 0;
        float speed = isSprinting ? runSpeed : walkSpeed;

        Vector3 moveDir = transform.right * h + transform.forward * v;
        moveDir.Normalize();

        isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;

        if (isGrounded && keyboard.spaceKey.wasPressedThisFrame)
        {
            verticalVelocity = jumpForce;
            animator.SetTrigger("Jump");
        }

        verticalVelocity -= gravity * Time.deltaTime;
        moveDir.y = verticalVelocity;
        controller.Move(moveDir * speed * Time.deltaTime);
    }

    void HandleStamina()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        bool isSprinting = keyboard.leftShiftKey.isPressed && stamina > 0;

        if (isSprinting)
            stamina -= staminaDrain * Time.deltaTime;
        else
            stamina = Mathf.Min(stamina + staminaRegen * Time.deltaTime, maxStamina);

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);

        if (staminaBar != null)
            staminaBar.value = stamina;
    }

    void HandleAnimations()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float h = 0f, v = 0f;
        if (keyboard.aKey.isPressed) h = -1f;
        if (keyboard.dKey.isPressed) h = 1f;
        if (keyboard.sKey.isPressed) v = -1f;
        if (keyboard.wKey.isPressed) v = 1f;

        bool isSprinting = keyboard.leftShiftKey.isPressed && stamina > 0;
        float speed = isSprinting ? runSpeed : walkSpeed;

        float currentSpeed = new Vector2(h, v).magnitude * speed;
        animator.SetFloat("Speed", currentSpeed, 0.1f, Time.deltaTime);
    }

    public void TriggerLoseAnimation()
    {
        animator.SetTrigger("Lose");
    }

    public void TriggerWinAnimation()
    {
        animator.SetTrigger("Win");
    }
}
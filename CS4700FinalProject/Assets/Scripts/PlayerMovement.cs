using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    public float maxStamina = 100f;
    public float staminaDrainRate = 20f; // per second
    public float staminaRegenRate = 5f; // per second
    public float staminaRegenDelay = 3f; // delay before regen starts
    public float exhaustionThreshold = 15f;
    private bool exhausted = false;
    private float currentStamina;
    private float regenTimer = 0f;
    public Slider staminaSlider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift);
        bool isSprinting = wantsToSprint && currentStamina > 0 && !exhausted;
        rb.velocity = moveInput * (isSprinting ? sprintSpeed : walkSpeed);
        staminaSlider.value = currentStamina;
        HandleStamina(isSprinting);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void HandleStamina(bool isSprinting)
    {
        if (isSprinting && !exhausted)
        {
            regenTimer = 0f;
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina <= 0f)
            {
                currentStamina = 0f;
                exhausted = true;
            }
        }
        else
        {
            regenTimer += Time.deltaTime;

            if (regenTimer >= staminaRegenDelay)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            }
        }

        if (exhausted && currentStamina >= exhaustionThreshold)
        {
            exhausted = false;
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public InputAction playerInput;
    public float suPeed = 100f;

    private void Start()
    {
        playerInput.Enable();
    }

    void Update()
    {
        if (Camera.main == null) return;

        Vector2 inputDirection = playerInput.ReadValue<Vector2>();

        // 1. ดึงทิศทาง Forward และ Right ของกล้องมา
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        // 2. ล็อกแกน Y ให้เป็น 0 เพื่อไม่ให้เคลื่อนที่ในแนวตั้ง
        camForward.y = 0f;
        camRight.y = 0f;

        // 3. ปรับค่า Vector ให้มีความยาว 1 เท่าเดิม
        camForward.Normalize();
        camRight.Normalize();

        // 4. คำนวณทิศทางเดินแนวราบเท่านั้น
        Vector3 moveDirection = (camForward * inputDirection.y) + (camRight * inputDirection.x);

        transform.forward = camForward;

        rb.AddForce(moveDirection * suPeed, ForceMode.Acceleration);
        rb.maxLinearVelocity = suPeed;

        if (inputDirection == Vector2.zero)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
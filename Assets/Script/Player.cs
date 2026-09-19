
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public InputAction playerInput;
    public float suPeed = 10f;

    private Vector2 inputDirection;

    private void Start()
    {
        playerInput.Enable();

        // ตรวจสอบว่า Rigidbody ถูกกำหนดแล้ว
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Camera.main == null) return;

        inputDirection = playerInput.ReadValue<Vector2>();

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection =
            camForward * inputDirection.y +
            camRight * inputDirection.x;

        if (moveDirection.sqrMagnitude > 0f)
            transform.forward = camForward;
    }

    private void FixedUpdate()
    {
        if (rb == null || Camera.main == null) return;

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection =
            camForward * inputDirection.y +
            camRight * inputDirection.x;

        if (inputDirection != Vector2.zero)
        {
            rb.AddForce(
                moveDirection * suPeed,
                ForceMode.Acceleration
            );
        }
        else
        {
            // หยุดเฉพาะแนวราบ ไม่หยุดการตก
            rb.linearVelocity = new Vector3(
                0f,
                rb.linearVelocity.y,
                0f
            );
        }
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}

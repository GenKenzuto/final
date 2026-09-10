using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Settings")]
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // ถ้าไม่ได้ลาก playerBody มาใน Inspector ให้หาจาก Tag "Player" อัตโนมัติ
        if (playerBody == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerBody = playerObj.transform;
            }
        }
    }

    void LateUpdate() // เปลี่ยนเป็น LateUpdate เพื่อให้กล้องขยับตามหลัง Player ลดอาการกล้องตุก
    {
        if (playerBody == null)
        {
            // ลองหา Player อีกครั้งกรณีที่ Instantiate ช้ากว่า Start
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) playerBody = playerObj.transform;
            return;
        }

        // ย้ายตำแหน่งกล้องไปที่ Player
        transform.position = playerBody.position + Vector3.up * 1.6f;

        if (Mouse.current != null && Mouse.current.rightButton.isPressed)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
            float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -89.9f, 89.9f);
            yRotation += mouseX;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
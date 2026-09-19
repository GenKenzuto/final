
using TMPro;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject playerPrefab;
    public Transform spawnPoint;

    [Header("UI Settings")]
    public GameObject KeyPanel;

    public GameObject WinnerPanel;

    public TMP_Text messageText;

    [Header("Timer Settings")]
    public TMP_Text timerText;
    public TMP_Text winnerTimeText;

    public GameObject TimingPanel;
    private bool hasWon = false;

    private float elapsedTime = 0f;
    private bool timerRunning = false;
    public static MazeManager Instance;

    private bool hasKey = false;

    void Awake()
    {
        if (TimingPanel != null)
            TimingPanel.SetActive(true);

        // ซ่อนหน้าชนะตอนเริ่มเกม
        if (WinnerPanel != null)
            WinnerPanel.SetActive(false);

        // เริ่มจับเวลา
        elapsedTime = 0f;
        timerRunning = true;
        if (KeyPanel != null)
            KeyPanel.SetActive(false);

        if (WinnerPanel != null)
            WinnerPanel.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
        }

        if (playerPrefab == null || spawnPoint == null)
        {
            Debug.LogError(
                "กรุณากำหนด Player Prefab และ Spawn Point ใน Inspector!"
            );
            return;
        }


        // สร้าง Player เหนือแผ่น Spawn Point
        GameObject player = Instantiate(
     playerPrefab,
     spawnPoint.position,
     spawnPoint.rotation
 );


        // ค้นหากล้องหลักใน Scene
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            PlayerCameraController cameraController =
                mainCamera.GetComponent<PlayerCameraController>();

            if (cameraController != null)
            {
                cameraController.SetTarget(player.transform);
            }
            else
            {
                Debug.LogError(
                    "ไม่พบ PlayerCameraController บนกล้องหลัก!"
                );
            }
        }
        else
        {
            Debug.LogError("ไม่พบกล้อง MainCamera!");
        }
    }
    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = "Time: " + FormatTime(elapsedTime);
    }

    private string FormatTime(float time)
    {
        int totalSeconds = Mathf.FloorToInt(time);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return minutes.ToString("00") + ":" +
               seconds.ToString("00");
    }
    public void OnKeyCollected(GameObject keyObject)
    {
        hasKey = true;
        Debug.Log("เก็บกุญแจเรียบร้อย!");
        Destroy(keyObject);
        KeyPanel.SetActive(true);
    }

    public void OnReachExit()
    {
        if (hasKey)
        {
            // หยุดจับเวลาเมื่อชนะ
            timerRunning = false;
            hasWon = true;

            string finalTime = FormatTime(elapsedTime);

            if (TimingPanel != null)
                TimingPanel.SetActive(false);

            if (WinnerPanel != null)
                WinnerPanel.SetActive(true);

            if (winnerTimeText != null)
                winnerTimeText.text = "Your Time: " + finalTime;

            Debug.Log("ยินดีด้วย! คุณผ่านเขาวงกตแล้ว");
            Debug.Log("เวลาที่ใช้: " + finalTime);
        }
        else
        {
            Debug.Log("ประตูยังล็อกอยู่! ต้องหากุญแจก่อน");
        }
    }
}


using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject playerPrefab;
    public Transform spawnPoint;

    public static MazeManager Instance;

    private bool hasKey = false;

    void Awake()
    {
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

    public void OnKeyCollected(GameObject keyObject)
    {
        hasKey = true;
        Debug.Log("เก็บกุญแจเรียบร้อย!");
        Destroy(keyObject);
    }

    public void OnReachExit()
    {
        if (hasKey)
        {
            Debug.Log("ยินดีด้วย! คุณผ่านเขาวงกตแล้ว");
        }
        else
        {
            Debug.Log("ประตูยังล็อกอยู่! ต้องหากุญแจก่อน");
        }
    }
}

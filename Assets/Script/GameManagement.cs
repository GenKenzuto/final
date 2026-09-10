using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject playerPrefab;
    public Transform spawnPoint;

    public static MazeManager Instance;

    private bool hasKey = false;

    void Awake() // เปลี่ยนเป็น Awake เพื่อสร้าง Player ให้เสร็จก่อนที่ Start ของสคริปต์อื่นจะทำงาน
    {
        if (Instance == null) Instance = this;

        if (playerPrefab != null && spawnPoint != null)
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("กรุณาใส่ Player Prefab หรือ Spawn Point ใน Inspector ให้ครบถ้วน!");
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
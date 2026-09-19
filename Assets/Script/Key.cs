
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (MazeManager.Instance != null)
            {
                MazeManager.Instance.OnKeyCollected(gameObject);
            }
        }
    }
}

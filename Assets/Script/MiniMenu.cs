using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.Search;
using UnityEngine.UI;

public class MiniMenu : MonoBehaviour
{
    public GameObject miniMenuPanel;
    public GameObject BiggerMiniMenuPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        miniMenuPanel.SetActive(true);
        BiggerMiniMenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool iswin = MazeManager.Instance.hasWon;
        if (iswin)
        {
            miniMenuPanel.SetActive(false);
            BiggerMiniMenuPanel.SetActive(false);
        }
    }

    public void ShowBiggerMiniMenu()
    {
        miniMenuPanel.SetActive(false);
        BiggerMiniMenuPanel.SetActive(true);
    }
    public void HideBiggerMiniMenu()
    {
        miniMenuPanel.SetActive(true);
        BiggerMiniMenuPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void restart()
    {
        SceneManager.LoadScene("Loading");
    }

}

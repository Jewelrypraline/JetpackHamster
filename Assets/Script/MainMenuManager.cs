using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Settings")]
    public string gameSceneName = "GameScene"; // พิมพ์ชื่อซีนเล่นเกมให้ตรงกับใน Unity

    [Header("UI Panels")]
    public GameObject aboutPanel; // ลาก UI Panel ของหน้า About มาวางช่องนี้

    void Start()
    {
        // ปิดหน้า About ไว้ก่อนเมื่อเริ่มเกม
        if (aboutPanel != null)
        {
            aboutPanel.SetActive(false);
        }
    }

    // ฟังก์ชันผูกกับปุ่ม PLAY
    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // ฟังก์ชันผูกกับปุ่ม ABOUT
    public void OpenAbout()
    {
        if (aboutPanel != null)
        {
            aboutPanel.SetActive(true);
        }
    }

    // ฟังก์ชันผูกกับปุ่มปิดหน้า ABOUT (เช่น ปุ่ม X หรือ Back)
    public void CloseAbout()
    {
        if (aboutPanel != null)
        {
            aboutPanel.SetActive(false);
        }
    }
}

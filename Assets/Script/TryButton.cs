using UnityEngine;
using UnityEngine.SceneManagement;

public class TryButton : MonoBehaviour
{
    [Header("Scene Settings")]
    public string mainMenuSceneName = "MainMenu"; // ชื่อซีนหน้าเมนูหลัก

    // ฟังก์ชันสั่งย้อนกลับไปหน้าเมนู
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // ฟังก์ชันสำหรับปุ่ม Try Again (เล่นใหม่อีกรอบในด่านเดิม)
    public void RestartGame()
    {
        // โหลดซีนปัจจุบันซ้ำอีกครั้ง
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

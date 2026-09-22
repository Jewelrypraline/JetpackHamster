using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject winPanel;

    private bool isWon = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public void TriggerWin()
    {
        if (isWon) return;
        isWon = true;

        if (winPanel != null)
        {
            winPanel.SetActive(true); // แสดงหน้าต่างชนะ
        }

        Time.timeScale = 0f; // หยุดเวลาและฟิสิกส์ทั้งหมดในเกม
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // คืนค่าเวลาเกมให้วิ่งปกติ
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // โหลดซีนปัจจุบันใหม่
    }

    // ฟังก์ชันสำหรับปุ่ม Main Menu
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // เปลี่ยนชื่อซีนให้ตรงกับหน้าเมนูของคุณ
    }
}

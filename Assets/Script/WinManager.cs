using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ถ้าเอาสคริปต์นี้แปะไว้ที่เสาเหลือง แล้วผู้เล่นเดินมาชน
        if (other.CompareTag("Player") || other.CompareTag("WinTrigger"))
        {
            TriggerWin();
        }
    }

    // เปิดเป็น public เพื่อให้สคริปต์อื่น (เช่น WinTrigger) เรียกสั่งงานได้ด้วย
    public void TriggerWin()
    {
        Time.timeScale = 1f; // คืนค่าเวลาให้เกมวิ่งปกติ
        SceneManager.LoadScene("WinScene"); // เปลี่ยนไปหน้า winscene
    }
}

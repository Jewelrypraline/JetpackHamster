using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public WinManager winManager; // บรรทัดนี้ประกาศไว้ตัวเดียวพอครับ

    void Start()
    {
        // ดึง WinManager ในฉากให้อัตโนมัติกันลืมลากใส่
        if (winManager == null)
        {
            winManager = FindFirstObjectByType<WinManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (winManager != null)
            {
                winManager.TriggerWin();
            }
        }
    }
}

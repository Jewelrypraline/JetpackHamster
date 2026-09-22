using UnityEngine;

public class BGTrigger : MonoBehaviour
{
    [Header("Background Settings")]
    public BackgroundManager bgManager;
    public Sprite newBgSprite; // รูปภาพใหม่ที่จะเปลี่ยนเมื่อผู้เล่นเดินผ่านจุดนี้

    private void Start()
    {
        // ค้นหา BackgroundManager อัตโนมัติหากไม่ได้ลากใส่
        if (bgManager == null)
        {
            bgManager = FindFirstObjectByType<BackgroundManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (bgManager != null && newBgSprite != null)
            {
                bgManager.ChangeBackground(newBgSprite);
            }
        }
    }
}

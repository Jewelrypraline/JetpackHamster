using UnityEngine;

public class BGTrigger : MonoBehaviour
{
    [Header("Background Settings")]
    public BackgroundManager bgManager;
    public Sprite newBgSprite; // รูปภาพใหม่ที่จะเปลี่ยนเมื่อผู้เล่นเดินผ่านจุดนี้

    [Header("Trigger Options")]
    public bool triggerOnce = true; // ทำงานครั้งเดียวเมื่อเดินผ่าน ป้องกันรูปเฟดซ้ำ
    private bool hasTriggered = false;

    private void Start()
    {
        // ค้นหา BackgroundManager อัตโนมัติหากไม่ได้ลากใส่ใน Inspector
        if (bgManager == null)
        {
            bgManager = FindFirstObjectByType<BackgroundManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && triggerOnce) return;

        // ตรวจจับว่าวัตถุที่มาชนมี Tag เป็น Player หรือไม่
        if (other.CompareTag("Player"))
        {
            if (bgManager != null && newBgSprite != null)
            {
                bgManager.ChangeBackground(newBgSprite);
                hasTriggered = true;
            }
        }
    }
}

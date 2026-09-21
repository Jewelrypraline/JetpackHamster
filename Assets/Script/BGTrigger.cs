using UnityEngine;

public class BGTrigger : MonoBehaviour
{
    [Header("BackgroundManager Code")]
    public BackgroundManager bgManager;

    [Header("ChangeBG")]
    public Sprite newBackgroundSprite;

    private bool hasTriggered = false; // ป้องกันไม่ให้ทำงานซ้ำ

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // ล็อคไว้ทำงานรอบเดียว

            if (bgManager != null && newBackgroundSprite != null)
            {
                bgManager.ChangeBackground(newBackgroundSprite);
            }
        }
    }
}

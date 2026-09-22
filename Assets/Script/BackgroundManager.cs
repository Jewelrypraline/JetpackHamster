using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundManager : MonoBehaviour
{
    [Header("UI Image References")]
    public Image bgCurrent;
    public Image bgNext;

    [Header("Transition Settings")]
    public float fadeDuration = 2.0f; // ปรับเป็น 2 วินาทีเพื่อให้เห็นการเฟดชัดๆ

    private bool isTransitioning = false;

    void Start()
    {
        if (bgNext != null)
        {
            Color c = bgNext.color;
            c.a = 0f;
            bgNext.color = c;
        }
    }

    public void ChangeBackground(Sprite newBgSprite)
    {
        if (!isTransitioning)
        {
            StartCoroutine(FadeToNewBackground(newBgSprite));
        }
    }

    private IEnumerator FadeToNewBackground(Sprite newBgSprite)
    {
        isTransitioning = true;

        // 1. ตั้งค่ารูปใหม่ และรีเซ็ต Alpha ของ bgNext ให้เป็น 0 ชัวร์ๆ
        bgNext.sprite = newBgSprite;
        Color startColor = bgNext.color;
        startColor.a = 0f;
        bgNext.color = startColor;

        // 2. ค่อยๆ ปรับ Alpha เพิ่มจาก 0 -> 1
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);

            Color currentNextColor = bgNext.color;
            currentNextColor.a = alpha;
            bgNext.color = currentNextColor;

            yield return null; // รอเฟรมถัดไป
        }

        // 3. พอสว่างเต็มที่แล้ว ค่อยย้ายรูปไปไว้ที่ bgCurrent แล้วซ่อน bgNext
        bgCurrent.sprite = newBgSprite;

        Color endColor = bgNext.color;
        endColor.a = 0f;
        bgNext.color = endColor;

        isTransitioning = false;
    }
}

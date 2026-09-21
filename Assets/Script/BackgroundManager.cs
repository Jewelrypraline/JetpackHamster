using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundManager : MonoBehaviour
{
    [Header("UI Image References")]
    public Image bgCurrent; // ลาก Bg_Current จาก CanvasBG มาใส่
    public Image bgNext;    // ลาก Bg_Next จาก CanvasBG มาใส่

    [Header("Transition Settings")]
    public float fadeDuration = 1.5f; // ระยะเวลาเฟด (ยิ่งเยอะยิ่งช้า)

    private bool isTransitioning = false;

    void Start()
    {
        // เริ่มต้นเซ็ตให้ bgNext โปร่งใส (Alpha = 0)
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

        // 1. ใส่รูปใหม่เข้าที่ bgNext
        bgNext.sprite = newBgSprite;

        // 2. ค่อยๆ ปรับ Alpha ของ bgNext จาก 0 เป็น 1
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);

            Color nextColor = bgNext.color;
            nextColor.a = alpha;
            bgNext.color = nextColor;

            yield return null;
        }

        // 3. พอ Fade สว่างเต็มที่ เปลี่ยนรูป bgCurrent เป็นรูปใหม่ แล้วรีเซ็ต bgNext กลับเป็นโปร่งใส
        bgCurrent.sprite = newBgSprite;

        Color resetNextColor = bgNext.color;
        resetNextColor.a = 0f;
        bgNext.color = resetNextColor;

        isTransitioning = false;
    }
}

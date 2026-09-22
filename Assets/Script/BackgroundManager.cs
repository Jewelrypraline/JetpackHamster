using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundManager : MonoBehaviour
{
    [Header("UI Image References")]
    public Image bgCurrent; // ประกาศตัวแปรอ้างอิงรูปปัจจุบัน
    public Image bgNext;    // ประกาศตัวแปรอ้างอิงรูปถัดไป

    [Header("Default Background Settings")]
    public Sprite defaultSprite; // ลากรูปใหม่ที่ต้องการใช้เริ่มต้นมาวางช่องนี้

    [Header("Transition Settings")]
    public float fadeDuration = 2.0f;

    private bool isTransitioning = false;

    void Start()
    {
        // 1. บังคับเปลี่ยนรูป bgCurrent เป็นรูปใหม่ทันทีเมื่อเริ่มเกม
        if (defaultSprite != null && bgCurrent != null)
        {
            bgCurrent.sprite = defaultSprite;
            Color c = bgCurrent.color;
            c.a = 1f;
            bgCurrent.color = c;
        }

        // 2. ซ่อน bgNext ไว้ก่อน
        if (bgNext != null)
        {
            Color c = bgNext.color;
            c.a = 0f;
            bgNext.color = c;
        }
    }

    public void ChangeBackground(Sprite newBgSprite)
    {
        if (!isTransitioning && newBgSprite != null)
        {
            StartCoroutine(CrossFadeBackground(newBgSprite));
        }
    }

    private IEnumerator CrossFadeBackground(Sprite newBgSprite)
    {
        isTransitioning = true;

        bgNext.sprite = newBgSprite;

        Color currentColor = bgCurrent.color;
        Color nextColor = bgNext.color;

        currentColor.a = 1f;
        nextColor.a = 0f;

        bgCurrent.color = currentColor;
        bgNext.color = nextColor;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / fadeDuration);

            currentColor.a = 1f - progress;
            nextColor.a = progress;

            bgCurrent.color = currentColor;
            bgNext.color = nextColor;

            yield return null;
        }

        bgCurrent.sprite = newBgSprite;
        currentColor.a = 1f;
        bgCurrent.color = currentColor;

        nextColor.a = 0f;
        bgNext.color = nextColor;

        isTransitioning = false;
    }
}

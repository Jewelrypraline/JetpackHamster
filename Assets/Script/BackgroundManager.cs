using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundManager : MonoBehaviour
{
    [Header("UI Image References")]
    public Image bgCurrent;
    public Image bgNext;

    [Header("Transition Settings")]
    public float fadeDuration = 2.0f;

    private bool isTransitioning = false;

    void Start()
    {
        // เริ่มต้นให้ bgNext ซ่อนไว้ก่อน
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
            StartCoroutine(CrossFadeBackground(newBgSprite));
        }
    }

    private IEnumerator CrossFadeBackground(Sprite newBgSprite)
    {
        isTransitioning = true;

        // ใส่รูปใหม่ให้ bgNext และตั้ง Alpha เริ่มต้นเป็น 0
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

            // ค่อยๆ ลด Alpha ภาพเก่า และเพิ่ม Alpha ภาพใหม่
            currentColor.a = 1f - progress;
            nextColor.a = progress;

            bgCurrent.color = currentColor;
            bgNext.color = nextColor;

            yield return null;
        }

        // รีเซ็ตค่าเมื่อเฟดเสร็จสิ้น
        bgCurrent.sprite = newBgSprite;
        currentColor.a = 1f;
        bgCurrent.color = currentColor;

        nextColor.a = 0f;
        bgNext.color = nextColor;

        isTransitioning = false;
    }
}

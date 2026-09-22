using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    // คลาสชุดข้อมูลความหิว
    [Serializable]
    public class HungerStat
    {
        public float maxHunger = 100f;
        public float currentHunger = 100f;
        public float decreaseRate = 3f;
        public Image fillImage;
    }

    // คลาสชุดข้อมูลฉี่/อึ
    [Serializable]
    public class PeeStat
    {
        public float maxPee = 100f;
        public float currentPee = 0f;
        public float increaseRate = 2f;
        public Image fillImage;
    }

    [Header("Status Bars")]
    public HungerStat hunger = new HungerStat();
    public PeeStat pee = new PeeStat();

    [Header("Smooth UI")]
    public float smoothSpeed = 8f;

    [Header("Jump / Movement Settings")]
    public float normalJumpForce = 10f;
    public float currentJumpForce;

    [Header("Scene Settings")]
    public string gameOverSceneName = "GameOver"; // ชื่อซีน GameOver (ปรับให้ตรงกับชื่อซีนใน Unity ได้)

    private bool isDead = false;

    void Start()
    {
        currentJumpForce = normalJumpForce;

        if (hunger.fillImage != null)
            hunger.fillImage.fillAmount = hunger.currentHunger / hunger.maxHunger;

        if (pee.fillImage != null)
            pee.fillImage.fillAmount = pee.currentPee / pee.maxPee;
    }

    void Update()
    {
        if (isDead) return;

        // ความหิวลดลงเรื่อยๆ
        if (hunger.currentHunger > 0)
        {
            hunger.currentHunger -= hunger.decreaseRate * Time.deltaTime;
            hunger.currentHunger = Mathf.Clamp(hunger.currentHunger, 0, hunger.maxHunger);
        }
        else if (hunger.currentHunger <= 0)
        {
            Die(); // หิวจนหมด -> ตาย
        }

        // ปวดฉี่สะสมเพิ่มขึ้นเรื่อยๆ
        if (pee.currentPee < pee.maxPee)
        {
            pee.currentPee += pee.increaseRate * Time.deltaTime;
            pee.currentPee = Mathf.Clamp(pee.currentPee, 0, pee.maxPee);
        }
        else if (pee.currentPee >= pee.maxPee)
        {
            Die(); // ปวดฉี่จนเต็ม (ฉี่แตก) -> ตายและย้ายไปซีน GameOver
        }

        UpdateUISmoothly();
    }

    void UpdateUISmoothly()
    {
        if (hunger.fillImage != null)
        {
            float targetFill = hunger.currentHunger / hunger.maxHunger;
            hunger.fillImage.fillAmount = Mathf.Lerp(hunger.fillImage.fillAmount, targetFill, Time.deltaTime * smoothSpeed);
        }

        if (pee.fillImage != null)
        {
            float targetFill = pee.currentPee / pee.maxPee;
            pee.fillImage.fillAmount = Mathf.Lerp(pee.fillImage.fillAmount, targetFill, Time.deltaTime * smoothSpeed);
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        // สั่งเปลี่ยนฉากไปหน้า GameOver ทันที
        SceneManager.LoadScene(gameOverSceneName);
    }

    // ฟังก์ชันสั่งลดค่าฉี่ (ใช้ตอนยืนบนถาดฉี่)
    public void EmptyPee(float emptyRate)
    {
        if (isDead) return;
        pee.currentPee -= emptyRate * Time.deltaTime;
        pee.currentPee = Mathf.Clamp(pee.currentPee, 0, pee.maxPee);
    }

    public void EatItem(float hungerAmount, float peeAmount)
    {
        if (isDead) return;
        hunger.currentHunger = Mathf.Clamp(hunger.currentHunger + hungerAmount, 0, hunger.maxHunger);
        pee.currentPee = Mathf.Clamp(pee.currentPee + peeAmount, 0, pee.maxPee);
    }
}

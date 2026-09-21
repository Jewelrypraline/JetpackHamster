using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    // --- คลาสชุดข้อมูลความหิว ---
    [Serializable]
    public class HungerStat
    {
        public float maxHunger = 100f;
        public float currentHunger = 100f; // เริ่มต้นที่ 100
        public float decreaseRate = 3f;     // หิวลงเรื่อยๆ
        public Image fillImage;
    }

    // --- คลาสชุดข้อมูลฉี่/อึ ---
    [Serializable]
    public class PeeStat
    {
        public float maxPee = 100f;
        public float currentPee = 0f;      // เปลี่ยนเริ่มต้นเป็น 0
        public float increaseRate = 2f;     // อัตราการปวดฉี่เพิ่มขึ้นต่อวิ
        public Image fillImage;
    }

    [Header("Status Bars")]
    public HungerStat hunger = new HungerStat();
    public PeeStat pee = new PeeStat();

    [Header("Smooth UI")]
    public float smoothSpeed = 8f; // ปรับให้ไวขึ้นนิดนึงตอนระบายฉี่จะได้ตอบสนองทันใจ

    void Start()
    {
        if (hunger.fillImage != null)
            hunger.fillImage.fillAmount = hunger.currentHunger / hunger.maxHunger;

        if (pee.fillImage != null)
            pee.fillImage.fillAmount = pee.currentPee / pee.maxPee;
    }

    void Update()
    {
        // 1. ความหิวลดลงเรื่อยๆ
        if (hunger.currentHunger > 0)
        {
            hunger.currentHunger -= hunger.decreaseRate * Time.deltaTime;
            hunger.currentHunger = Mathf.Clamp(hunger.currentHunger, 0, hunger.maxHunger);
        }

        // 2. ปวดฉี่สะสมเพิ่มขึ้นเรื่อยๆ
        if (pee.currentPee < pee.maxPee)
        {
            pee.currentPee += pee.increaseRate * Time.deltaTime;
            pee.currentPee = Mathf.Clamp(pee.currentPee, 0, pee.maxPee);
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

    // ฟังก์ชันสั่งลดค่าฉี่ (ใช้ตอนยืนบนถาดฉี่)
    public void EmptyPee(float emptyRate)
    {
        pee.currentPee -= emptyRate * Time.deltaTime;
        pee.currentPee = Mathf.Clamp(pee.currentPee, 0, pee.maxPee);
    }

    public void EatItem(float hungerAmount, float peeAmount)
    {
        hunger.currentHunger = Mathf.Clamp(hunger.currentHunger + hungerAmount, 0, hunger.maxHunger);
        pee.currentPee = Mathf.Clamp(pee.currentPee + peeAmount, 0, pee.maxPee);
    }
}

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
    public float smoothSpeed = 5f;

    void Start()
    {
        // บังคับเซ็ตค่าเริ่มต้นให้ตรงเป๊ะตั้งแต่เฟรมแรก ไม่ต้องรอเกลี่ย Lerp
        if (hunger.fillImage != null)
        {
            hunger.fillImage.fillAmount = hunger.currentHunger / hunger.maxHunger;
        }

        if (pee.fillImage != null)
        {
            pee.fillImage.fillAmount = pee.currentPee / pee.maxPee; // ปวดฉี่เริ่ม 0 หลอดจะเป็น 0 ทันที
        }
    }

    void Update()
    {
        // 1. ความหิวลดลง (100 -> 0)
        if (hunger.currentHunger > 0)
        {
            hunger.currentHunger -= hunger.decreaseRate * Time.deltaTime;
            hunger.currentHunger = Mathf.Clamp(hunger.currentHunger, 0, hunger.maxHunger);
        }

        // 2. ปวดฉี่สะสมเพิ่มขึ้น (0 -> 100)
        if (pee.currentPee < pee.maxPee)
        {
            pee.currentPee += pee.increaseRate * Time.deltaTime;
            pee.currentPee = Mathf.Clamp(pee.currentPee, 0, pee.maxPee);
        }

        // 3. ค่อยๆ เกลี่ย UI นุ่มๆ ระหว่างเล่น
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

    public void EatItem(float hungerAmount, float peeAmount)
    {
        hunger.currentHunger = Mathf.Clamp(hunger.currentHunger + hungerAmount, 0, hunger.maxHunger);
        pee.currentPee = Mathf.Clamp(pee.currentPee + peeAmount, 0, pee.maxPee);
    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    //คลาสชุดข้อมูลความหิว
    [Serializable]
    public class HungerStat
    {
        public float maxHunger = 100f;
        public float currentHunger = 100f;
        public float decreaseRate = 3f;
        public Image fillImage;
    }

    //คลาสชุดข้อมูลฉี่/อึ
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
    public float normalJumpForce = 10f; // แรงกระโดดปกติ
    public float currentJumpForce;      // แรงกระโดดปัจจุบันที่จะเอาไปใช้กับสคริปต์เดิน/บิน

    private bool isDead = false; // เช็คสถานะการตาย

    void Start()
    {
        currentJumpForce = normalJumpForce; //ตั้งค่าเริ่มต้นให้กระโดดได้ปกติ

        if (hunger.fillImage != null)
            hunger.fillImage.fillAmount = hunger.currentHunger / hunger.maxHunger;

        if (pee.fillImage != null)
            pee.fillImage.fillAmount = pee.currentPee / pee.maxPee;
    }

    void Update()
    {
        if (isDead) return; //ถ้าตายแล้วหยุดรันทุกอย่าง

        // ความหิวลดลงเรื่อยๆ
        if (hunger.currentHunger > 0)
        {
            hunger.currentHunger -= hunger.decreaseRate * Time.deltaTime;
            hunger.currentHunger = Mathf.Clamp(hunger.currentHunger, 0, hunger.maxHunger);
        }
        else if (hunger.currentHunger <= 0)
        {
            Die(); //ถ้าหิวจนหมดจะตาย
        }

        // ปวดฉี่สะสมเพิ่มขึ้นเรื่อยๆ
        if (pee.currentPee < pee.maxPee)
        {
            pee.currentPee += pee.increaseRate * Time.deltaTime;
            pee.currentPee = Mathf.Clamp(pee.currentPee, 0, pee.maxPee);
        }
        else if (pee.currentPee >= pee.maxPee)
        {
            PeePants(); //ฉี่แตก
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

    void PeePants()
    {
        hunger.currentHunger -= (hunger.maxHunger * 0.3f);
        hunger.currentHunger = Mathf.Clamp(hunger.currentHunger, 0, hunger.maxHunger);

        pee.currentPee = 0f;

        //เปลี่ยนจาก 1.00f เป็น 0.3f หรือต่ำกว่านี้ครับ เพื่อให้แรงกระโดดเหลือแค่ 30%
        currentJumpForce = normalJumpForce * 0.3f;

        StartCoroutine(ResetDebuff());
    }

    //ฟังก์ชันจับเวลา 10 วินาที
    private IEnumerator ResetDebuff()
    {
        yield return new WaitForSeconds(10f); //รอ 10 วินาที
        currentJumpForce = normalJumpForce; //รีเซ็ตค่ากระโดดกลับเป็นปกติ
    }

    void Die()
    {
        isDead = true;
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

using UnityEngine;

public class Toilet : MonoBehaviour
{
    [Header("Toilet Settings")]
    public float emptySpeed = 60f; // ความเร็วในการระบายฉี่ (ตั้งไว้ 60 = ใช้เวลาประมาณ 1.5 - 2 วินาทีจนหมด 100)
    public Transform snapPoint;    // จุดตรงกลางถาดฉี่ (ไว้ล็อคตำแหน่งหนู)
    public float snapSpeed = 10f;   // ความเร็วในการดูดหนูเข้าจุดล็อค

    private bool isPlayerOnTray = false;
    private PlayerStatus playerStatus;
    private Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnTray = true;
            playerStatus = other.GetComponent<PlayerStatus>();
            playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnTray = false;
            playerStatus = null;
            playerTransform = null;
        }
    }

    private void Update()
    {
        if (isPlayerOnTray && playerStatus != null)
        {
            // 1. ค่อยๆ ลดค่าฉี่ลงเรื่อยๆ ตราบใดที่ยังยืนแช่อยู่บนถาด
            if (playerStatus.pee.currentPee > 0)
            {
                playerStatus.EmptyPee(emptySpeed);
            }

            // 2. ดูดตัวหนูให้เข้าตรงกลางถาดฉี่เนียนๆ (ถ้าตั้ง snapPoint ไว้)
            if (snapPoint != null && playerTransform != null)
            {
                // ดูดเฉพาะแกน X กับ Z ให้ตรงล็อค
                Vector3 targetPos = new Vector3(snapPoint.position.x, playerTransform.position.y, snapPoint.position.z);
                playerTransform.position = Vector3.Lerp(playerTransform.position, targetPos, Time.deltaTime * snapSpeed);
            }
        }
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public PlayerStatus statusScript;

    [Header("Floaty Jump Settings")]
    public float riseAcceleration = 12f; // ความไวในการเร่งลอยขึ้น (ยิ่งค่าน้อย ยิ่งลอยขึ้นอืด/ช้า)

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (statusScript == null) statusScript = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        if (rb == null || statusScript == null) return;

        // เมื่อกดหรือกดสเปซบาร์ค้าง/ย้ำๆ
        if (Input.GetKey(KeyCode.Space))
        {
            // ค่อยๆ ปรับความเร็ว Y ให้ไต่ระดับขึ้นไปหาค่า currentJumpForce แบบอืดๆ ช้าๆ
            float targetY = statusScript.currentJumpForce;
            float newY = Mathf.MoveTowards(rb.linearVelocity.y, targetY, riseAcceleration * Time.deltaTime);

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, newY, rb.linearVelocity.z);
        }
    }
}

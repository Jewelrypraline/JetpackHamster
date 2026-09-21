using UnityEngine;

public class Grass : MonoBehaviour
{
    [Header("Grass Settings")]
    public float hungerRestoreAmount = 30f; // กินแล้วรีค่าหิวเท่าไหร่
    public float peeAmount = 0f;            // เติมค่าฉี่ด้วยไหม (ถ้าไม่ใช้ใส่ 0)

    [Header("3D Animation Settings")]
    public float rotateSpeed = 90f;     // ความเร็วในการหมุนรอบตัวเอง (องศา/วินาที)
    public float floatSpeed = 2f;       // ความเร็วในการลอยขึ้น-ลง
    public float floatAmplitude = 0.2f;  // ระยะความสูง-ต่ำของการลอย

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 1. หมุนวนรอบตัวเองช้าๆ (หมุนแกน Y)
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        // 2. ลอยขึ้น-ลง นุ่มๆ
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();

            if (playerStatus != null)
            {
                playerStatus.EatItem(hungerRestoreAmount, peeAmount);
                Destroy(gameObject); // ชนแล้วหายไป
            }
        }
    }
}

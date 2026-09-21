using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float moveSpeed = 5f;     // ความเร็วในการวิ่งไปทางขวา

    public float flyForce = 7f;      // แรงดันเจ็ทแพ็ทดันตัวขึ้น
    public bool isContinuousFly = false; // ติ๊กถูกถ้าอยากได้แบบ "กดค้างแล้วบินขึ้นเรื่อยๆ"

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isContinuousFly && Input.GetKeyDown(KeyCode.Space))
        {
            
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * flyForce, ForceMode.Impulse);
        }
    }

    void ForceMove()
    {
       
        rb.linearVelocity = new Vector3(moveSpeed, rb.linearVelocity.y, 0f);

        if (isContinuousFly && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * flyForce, ForceMode.Acceleration);
        }
    }
}

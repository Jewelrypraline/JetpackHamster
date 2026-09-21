using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    public float moveSpeed = 5f;     // ความเร็วในการวิ่งไปทางขวา

    [SerializeField]
    public float flyForce = 7f;      // แรงดันเจ็ทแพ็ทดันตัวขึ้น

    public bool autoRunRight = true;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(0f, flyForce, moveSpeed);
        }
    }

    void FixedUpdate()
    {
        if (autoRunRight)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, moveSpeed);
        }
    }
}

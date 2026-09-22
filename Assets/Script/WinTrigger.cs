using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public WinManager winManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (winManager != null)
            {
                winManager.TriggerWin(); // สั่งงานระบบชนะทันทีเมื่อชนเส้นชัย
            }
        }
    }
}

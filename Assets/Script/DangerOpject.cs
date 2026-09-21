using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerOpject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Danger"))
        {
            TriggerGameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Danger"))
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        Debug.Log("Game Over You got catch!");
        SceneManager.LoadScene("GameOver");
    }
}

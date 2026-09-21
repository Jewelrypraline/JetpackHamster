using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Danger"))
        {
            Debug.Log("Game Over You got catch!");

            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGameOver : MonoBehaviour
{
    void Update()
    {
        // Test GameOver dengan menekan G
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Testing GameOver - Loading Gameover scene...");
            SceneManager.LoadScene("Gameover");
        }
    }
}
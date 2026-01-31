using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button restartButton;
    public Button quitButton;

    void Start()
    {
        Debug.Log("GameOverManager started! Scene Gameover berhasil dimuat!");
        
        // Cari tombol secara otomatis jika tidak di-assign di inspector
        if (restartButton == null)
        {
            // Cari tombol dengan nama "RestartButton" atau yang mengandung kata "restart"
            Button[] buttons = FindObjectsOfType<Button>();
            foreach (Button btn in buttons)
            {
                if (btn.name.ToLower().Contains("restart"))
                {
                    restartButton = btn;
                    Debug.Log("Found restart button: " + btn.name);
                    break;
                }
            }
        }

        if (quitButton == null)
        {
            // Cari tombol dengan nama "QuitButton" atau yang mengandung kata "quit"
            Button[] buttons = FindObjectsOfType<Button>();
            foreach (Button btn in buttons)
            {
                if (btn.name.ToLower().Contains("quit") || btn.name.ToLower().Contains("exit"))
                {
                    quitButton = btn;
                    Debug.Log("Found quit button: " + btn.name);
                    break;
                }
            }
        }

        // Setup button listeners
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
            Debug.Log("Restart button listener added!");
        }
        else
        {
            Debug.LogWarning("Restart button not found! Assign it in the inspector or name it with 'restart'");
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
            Debug.Log("Quit button listener added!");
        }
        else
        {
            Debug.LogWarning("Quit button not found! Assign it in the inspector or name it with 'quit'");
        }
    }

    void Update()
    {
        // Restart dengan R key (backup jika tombol tidak berfungsi)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed - Restarting game...");
            RestartGame();
        }

        // Quit dengan Escape key (backup jika tombol tidak berfungsi)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed - Quitting game...");
            QuitGame();
        }
    }

    // dipanggil oleh tombol Restart
    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        SceneManager.LoadScene("Gameplay");
    }

    // dipanggil oleh tombol Quit
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

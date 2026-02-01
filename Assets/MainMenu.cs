using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartGameButton ()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}
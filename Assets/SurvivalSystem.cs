using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivalSystem : MonoBehaviour
{
    [Header("Oxygen")]
    public float maxOxygen = 10f;
    public float oxygenDrainPerSecond = 1f;

    [Header("Health")]
    public float maxHealth = 100f;
    public float healthDrainPerSecond = 5f;

    [Header("UI Bars (RectTransform)")]
    public RectTransform oxygenBar;
    public RectTransform healthBar;

    float currentOxygen;
    float currentHealth;
    bool gameOverTriggered = false; // Flag untuk mencegah GameOver dipanggil berulang

    void Start()
    {
        currentOxygen = maxOxygen;
        currentHealth = maxHealth;
        UpdateBars();
    }

    void Update()
    {
        DrainOxygen();
        DrainHealthIfNoOxygen();
        UpdateBars();
    }

    void DrainOxygen()
    {
        if (currentOxygen > 0)
        {
            currentOxygen -= oxygenDrainPerSecond * Time.deltaTime;
            currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen);
        }
    }

    void DrainHealthIfNoOxygen()
    {
        if (currentOxygen <= 0 && !gameOverTriggered)
        {
            currentHealth -= healthDrainPerSecond * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            
            // Debug untuk melihat nilai health
            Debug.Log("Current Health: " + currentHealth);
            
            // Cek apakah health sudah habis
            if (currentHealth <= 0)
            {
                Debug.Log("Health habis! Memanggil GameOver...");
                gameOverTriggered = true; // Set flag agar tidak dipanggil lagi
                GameOver();
            }
        }
    }

    void UpdateBars()
    {
        oxygenBar.localScale = new Vector3(
            currentOxygen / maxOxygen,
            1,
            1
        );

        healthBar.localScale = new Vector3(
            currentHealth / maxHealth,
            1,
            1
        );
    }

    public void AddOxygen(float amount)
    {
        currentOxygen = Mathf.Clamp(currentOxygen + amount, 0, maxOxygen);
        UpdateBars();
    }

    // 🔥 INI YANG KAMU MINTA
    public void RefillOxygen()
    {
        currentOxygen = maxOxygen;
        UpdateBars();
    }

    void GameOver()
    {
        Debug.Log("GameOver dipanggil! Player akan di-destroy...");
        
        // Cari player GameObject (biasanya yang memiliki PlayerMovement script)
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            Debug.Log("Player ditemukan!");
            
            // Cek apakah player memiliki PlayerDeath script
            PlayerDeath playerDeath = player.GetComponent<PlayerDeath>();
            if (playerDeath != null)
            {
                Debug.Log("Menggunakan PlayerDeath script untuk efek kematian...");
                playerDeath.TriggerDeath();
            }
            else
            {
                Debug.Log("PlayerDeath script tidak ditemukan, menggunakan destroy biasa...");
                // Fallback: destroy langsung dan pindah scene
                Destroy(player.gameObject);
                Invoke("LoadGameOverScene", 1f);
            }
        }
        else
        {
            Debug.LogWarning("Player tidak ditemukan! Langsung pindah ke GameOver scene...");
            LoadGameOverScene();
        }
    }
    
    void LoadGameOverScene()
    {
        Debug.Log("Loading GameOver scene...");
        
        try
        {
            SceneManager.LoadScene("Gameover");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error loading scene Gameover: " + e.Message);
            Debug.LogError("Pastikan scene 'Gameover' sudah ditambahkan ke Build Settings!");
        }
    }
}

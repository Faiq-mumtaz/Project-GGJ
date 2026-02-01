using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class SurvivalSystem : MonoBehaviour
{
    [Header("Oxygen")]
    public float maxOxygen = 10f;
    public float oxygenDrainPerSecond = 2f;

    [Header("Health")]
    public float maxHealth = 100f;
    public float healthDrainPerSecond = 5f;

    [Header("UI Bars (RectTransform)")]
    public Image oxygenBar;

    public Image healthBar;

    public TMP_Text oxygenCount;

    public TMP_Text healthCount;

    float currentOxygen;
    float currentHealth;
    bool isDead = false;

    void Start()
    {
        currentOxygen = maxOxygen;
        currentHealth = maxHealth;
        UpdateBars();
    }

    void Update()
    {
        if (isDead) return;

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
        if (currentOxygen <= 0)
        {
            currentHealth -= healthDrainPerSecond * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void UpdateBars()
    {
        if (oxygenBar != null)
        {
            oxygenBar.fillAmount = currentOxygen / maxOxygen;
        }

        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }

        if (oxygenCount != null)
        {
            oxygenCount.text = Mathf.FloorToInt(currentOxygen).ToString();
        }

        if (healthCount != null)
        {
            float percent = (currentHealth / maxHealth) * 100f;
            healthCount.text = Mathf.FloorToInt(percent) + "%";
        }
    }

    // ➕ Tambah oksigen sebagian (kalau nanti mau)
    public void AddOxygen(float amount)
    {
        currentOxygen = Mathf.Clamp(currentOxygen + amount, 0, maxOxygen);
        UpdateBars();
    }

    // 🛢️ Refill penuh (dipanggil tabung oksigen)
    public void RefillOxygen()
    {
        currentOxygen = maxOxygen;
        UpdateBars();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("PLAYER MATI - GAME OVER");

        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }
}

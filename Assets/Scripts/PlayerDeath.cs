using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    [Header("Death Effects")]
    public float deathDelay = 1f; // Delay sebelum pindah scene
    public bool fadeOut = true; // Fade out player sebelum destroy
    public float fadeSpeed = 2f; // Kecepatan fade
    
    private SpriteRenderer spriteRenderer;
    private bool isDying = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TriggerDeath()
    {
        if (isDying) return; // Prevent multiple calls
        
        isDying = true;
        Debug.Log("Player death triggered!");
        
        // Disable player movement
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
        
        // Disable rigidbody physics
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
        }
        
        // Start death sequence
        StartCoroutine(DeathSequence());
    }
    
    IEnumerator DeathSequence()
    {
        Debug.Log("Starting death sequence...");
        
        if (fadeOut && spriteRenderer != null)
        {
            // Fade out effect
            Color originalColor = spriteRenderer.color;
            float fadeTimer = 0f;
            
            while (fadeTimer < 1f)
            {
                fadeTimer += Time.deltaTime * fadeSpeed;
                Color newColor = originalColor;
                newColor.a = Mathf.Lerp(1f, 0f, fadeTimer);
                spriteRenderer.color = newColor;
                yield return null;
            }
        }
        
        // Wait for death delay
        yield return new WaitForSeconds(deathDelay);
        
        // Destroy player
        Debug.Log("Destroying player and loading GameOver scene...");
        Destroy(gameObject);
        
        // Load GameOver scene
        SceneManager.LoadScene("Gameover");
    }
}
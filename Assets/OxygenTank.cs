using UnityEngine;

public class OxygenPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SurvivalSystem survival = other.GetComponent<SurvivalSystem>();

            if (survival != null)
            {
                survival.RefillOxygen();
            }

            Destroy(gameObject);
        }
    }
}

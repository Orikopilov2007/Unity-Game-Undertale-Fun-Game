using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance; // Singleton instance

    public int maxHealth = 100;
    public int currentHealth;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if needed
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    void Start()
    {
        currentHealth = 92; // Start with health set to 92
        Debug.Log($"Starting health: {currentHealth}/{maxHealth}");
    }

    public void AddHP(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Cap health at maxHealth
        }
        Debug.Log($"Character healed by {healAmount} HP. Current health: {currentHealth}/{maxHealth}");
    }
}

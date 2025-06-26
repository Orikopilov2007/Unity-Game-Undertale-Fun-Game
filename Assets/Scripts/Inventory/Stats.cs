using UnityEngine;

public class Stats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Start with full health
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

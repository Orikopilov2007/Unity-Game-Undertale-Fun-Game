using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Stats playerStats; // Reference to PlayerStats component

    void Start()
    {
        // Example usage: Increase player's HP
        IncreasePlayerHP(50); // Increase player's HP by 50
    }

    void IncreasePlayerHP(int amount)
    {
        if (playerStats != null)
        {
            playerStats.AddHP(amount); // Call AddHP on the playerStats instance
        }
        else
        {
            Debug.LogWarning("PlayerStats component not found.");
        }
    }
}

using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public int healAmount = 20; // Amount of HP the food item heals

    // Method to use the food item
    public void Use(Stats character)
    {
        character.AddHP(healAmount);
        Debug.Log($"Used {gameObject.name}. Healed {healAmount} HP.");
        Destroy(gameObject); // Destroy the food item after use
    }

    // Optional: Trigger the healing when the player interacts with the food item
    private void OnTriggerEnter(Collider other)
    {
        Stats character = other.GetComponent<Stats>();
        if (character != null)
        {
            Use(character);
        }
    }
}

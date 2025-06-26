using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ScriptableObject item; // Reference to the Scriptable Object item

    private Image itemImage; // Reference to the Image component

    void Awake()
    {
        itemImage = GetComponent<Image>(); // Get the Image component attached to this GameObject
    }

    public void Use()
    {
        if (item != null)
        {
            // Check if the item has been destroyed
            if (!gameObject.activeInHierarchy)
            {
                Debug.LogWarning($"Trying to use {item.name}, but the item GameObject is inactive (possibly destroyed).");
                return;
            }

            // Implement item usage logic based on item properties
            Debug.Log($"Using {item.name}."); // Example: Log item usage

            // Example: Check item type and perform corresponding action
            if (item is ButterscotchPie)
            {
                PlayerStats.Instance.AddHP((item as ButterscotchPie).healAmount);
            }
            else if (item is SnowmanPiece)
            {
                PlayerStats.Instance.AddHP((item as SnowmanPiece).healAmount);
            }
            else if (item is LegendaryHero)
            {
                PlayerStats.Instance.AddHP((item as LegendaryHero).healAmount);
            }

            // Destroy item after use (optional)
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Trying to use an item, but the item reference is null.");
        }
    }

    public void Select()
    {
        // Implement visual feedback for selected item (e.g., change color, scale)
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = Color.yellow; // Change item color to yellow for selected item
        }
        else
        {
            Debug.LogWarning("Image component is missing on InventoryItem.");
            // Implement alternative visual feedback here if needed
        }
    }

    public void Deselect()
    {
        // Implement visual feedback for deselected item (e.g., revert color, scale)
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = Color.white; // Change item color back to white for deselected item
        }
        else
        {
            Debug.LogWarning("Image component is missing on InventoryItem.");
            // Implement alternative visual feedback here if needed
        }
    }
}

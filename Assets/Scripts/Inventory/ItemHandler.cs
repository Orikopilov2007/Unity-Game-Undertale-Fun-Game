using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    public ScriptableObject item; // Reference to the ScriptableObject item

    private Image itemImage; // Reference to the Image component

    void Awake()
    {
        itemImage = GetComponent<Image>(); // Get the Image component attached to this GameObject
    }

    public void UseItem()
    {
        if (item != null)
        {
            // Implement item usage logic based on item properties
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
    }

    public void Select()
    {
        // Implement visual feedback for selected item (e.g., change color, scale)
        itemImage.color = Color.yellow;
    }

    public void Deselect()
    {
        // Implement visual feedback for deselected item (e.g., revert color, scale)
        itemImage.color = Color.white;
    }
}

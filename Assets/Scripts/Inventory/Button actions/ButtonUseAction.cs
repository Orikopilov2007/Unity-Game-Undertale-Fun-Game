using UnityEngine;

public class ButtonUseAction : MonoBehaviour
{
    public InventoryManager inventoryManager; // Reference to the InventoryManager script

    private void Awake()
    {
        if (inventoryManager == null)
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
        }
    }

    public void OnUseButtonClicked()
    {
        if (inventoryManager != null)
        {
            int currentItemIndex = inventoryManager.GetCurrentItemIndex();
            if (currentItemIndex >= 0 && currentItemIndex < inventoryManager.itemSlots.Length)
            {
                inventoryManager.SelectButton(0); // Select the "Use" button (first action button)
            }
        }
    }

    public void OnInfoButtonClicked()
    {
        if (inventoryManager != null)
        {
            int currentItemIndex = inventoryManager.GetCurrentItemIndex();
            if (currentItemIndex >= 0 && currentItemIndex < inventoryManager.itemSlots.Length)
            {
                inventoryManager.SelectButton(1); // Select the "Info" button (second action button)
            }
        }
    }
}

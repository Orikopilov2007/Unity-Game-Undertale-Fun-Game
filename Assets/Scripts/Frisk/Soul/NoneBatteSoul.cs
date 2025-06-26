using UnityEngine;

public class NoneBattleSoul : MonoBehaviour
{
    public float moveSpeed = 100f; // Adjust as needed
    private RectTransform rectTransform;
    private Vector3 targetPosition;
    private Canvas canvas; // Reference to the Canvas component
    private InventoryManager inventoryManager;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); // Assuming NoneBattleSoul is under the Canvas
        inventoryManager = FindObjectOfType<InventoryManager>(); // Assuming InventoryManager is in the scene
    }

    void Update()
    {
        // Check if inventory is open and allow movement
        if (inventoryManager != null && inventoryManager.isInventoryOpen)
        {
            // Move up and down based on player input
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 currentPosition = rectTransform.anchoredPosition;
            targetPosition = currentPosition + new Vector3(0f, verticalInput * moveSpeed * Time.deltaTime, 0f);
            rectTransform.anchoredPosition = ClampPosition(targetPosition);

            // Handle button selection
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
            {
                inventoryManager.SelectButton(inventoryManager.GetCurrentItemIndex()); // Use current item index for selection
            }
        }
    }

    Vector3 ClampPosition(Vector3 position)
    {
        // Clamp the position to stay within the bounds of the inventory UI
        Vector3 minPosition = new Vector3(0f, -160f, 0f); // Adjust as needed based on inventory layout
        Vector3 maxPosition = new Vector3(0f, 160f, 0f); // Adjust as needed based on inventory layout
        return new Vector3(position.x, Mathf.Clamp(position.y, minPosition.y, maxPosition.y), position.z);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        rectTransform.anchoredPosition = targetPosition;
    }
}

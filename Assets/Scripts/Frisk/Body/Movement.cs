using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    private bool isMovingRight = false;
    private bool isMovingLeft = false;
    private bool isMovingUp = false;
    private bool isMovingDown = false;

    void Update()
    {
        if (!IsInventoryOpen())
        {
            bool newIsMovingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            bool newIsMovingLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            bool newIsMovingUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            bool newIsMovingDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

            // Set movement parameters based on input
            if (newIsMovingRight)
            {
                isMovingRight = true;
                isMovingLeft = false;
                isMovingUp = false;
                isMovingDown = false;
            }
            else if (newIsMovingLeft)
            {
                isMovingRight = false;
                isMovingLeft = true;
                isMovingUp = false;
                isMovingDown = false;
            }
            else if (newIsMovingUp)
            {
                isMovingRight = false;
                isMovingLeft = false;
                isMovingUp = true;
                isMovingDown = false;
            }
            else if (newIsMovingDown)
            {
                isMovingRight = false;
                isMovingLeft = false;
                isMovingUp = false;
                isMovingDown = true;
            }
            else
            {
                isMovingRight = false;
                isMovingLeft = false;
                isMovingUp = false;
                isMovingDown = false;
            }
        }
        else
        {
            // If inventory is open, stop movement
            isMovingRight = false;
            isMovingLeft = false;
            isMovingUp = false;
            isMovingDown = false;
        }

        // Update Animator parameters
        animator.SetBool("IsMovingRight", isMovingRight);
        animator.SetBool("IsMovingLeft", isMovingLeft);
        animator.SetBool("IsMovingUp", isMovingUp);
        animator.SetBool("IsMovingDown", isMovingDown);
    }

    bool IsInventoryOpen()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        return inventoryManager != null && inventoryManager.isInventoryOpen;
    }
}

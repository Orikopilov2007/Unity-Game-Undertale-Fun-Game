using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;
    public LayerMask boneLayer; // Assign this in the Inspector to the layer of your Bones
    public string nextSceneName; // Name of the scene to switch to
    public float switchXPosition = 903.8827f; // X position to trigger scene switch

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Check if the player's X position reaches the switch position
        if (transform.position.x >= switchXPosition)
        {
            SwitchScene();
        }
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        // Check for collision at the new position
        Collider2D hit = Physics2D.OverlapCircle(newPosition, 0.1f, boneLayer);
        if (hit == null)
        {
            // No collision, move the character
            rb.MovePosition(newPosition);
        }
        else
        {
            // Collision detected, prevent movement
            Debug.Log("Movement blocked");
        }
    }

    void SwitchScene()
    {
        // Switch to the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}

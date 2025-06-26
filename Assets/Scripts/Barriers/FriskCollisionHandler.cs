using UnityEngine;

public class FriskCollisionHandler : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bones"))
        {
            // Handle the collision, e.g., stop movement or bounce back
            Debug.Log("Frisk collided with Bones!");

            // Example: stop movement
            rb.velocity = Vector2.zero;
        }
    }
}

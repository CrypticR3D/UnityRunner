using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // Speed of movement

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Read horizontal input
        float movementInput = Input.GetAxis("Horizontal");

        // Calculate movement vector
        Vector2 movement = new Vector2(movementInput * movementSpeed, rb.velocity.y);

        // Apply movement to rigidbody
        rb.velocity = movement;
    }
}

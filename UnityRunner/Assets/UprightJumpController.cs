using UnityEngine;

public class UprightJumpController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f; // The force applied when jumping

    private Rigidbody2D rb;
    private bool isGrounded; // Tracks if the object is touching the ground

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Apply vertical force for jumping
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

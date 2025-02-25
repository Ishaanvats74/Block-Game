using UnityEngine;

public class JumpCube : MonoBehaviour
{
    public float jumpForce = 10.0f; // Adjust this value to change the jump force
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Check if the spacebar is pressed and the cube is on the ground
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // The cube is no longer grounded once it jumps
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the cube has collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Set isGrounded to true when the cube touches the ground
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the cube has left the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Set isGrounded to false when the cube leaves the ground
        }
    }
}

using UnityEngine;

public class CubeAndCameraScript : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;
    public float cameraOffsetZ = -10.0f; // Offset for the camera position
    public float raycastDistance = 2.0f; // Distance to check for obstacles
    private bool isGrounded = true; // To check if the cube is on the ground
    private Rigidbody rb; // Add a Rigidbody component to the Cube
    private GameObject mainCamera;
    private bool shouldStopForward = false; // Variable to control stopping forward movement

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Initialize Rigidbody component
        mainCamera = Camera.main.gameObject; // Reference to the Main Camera
        Debug.Log("CubeAndCameraScript started.");
    }

    void Update()
    {
        Debug.Log($"Update called: shouldStopForward = {shouldStopForward}, isGrounded = {isGrounded}");

        if (!shouldStopForward)
        {
            Debug.Log("Moving forward.");
            // Move cube forward continuously
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        // Handle input for left and right movement
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Moving left.");
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Moving right.");
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jumping.");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Raycast to check for obstacles ahead
        if (shouldStopForward)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
            {
                if (!hit.collider.CompareTag("Obstacle"))
                {
                    Debug.Log("No obstacle ahead. Resuming forward movement.");
                    shouldStopForward = false;
                }
            }
            else
            {
                Debug.Log("No obstacle detected within raycast distance. Resuming forward movement.");
                shouldStopForward = false;
            }
        }

        // Camera following cube on z-axis only
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.z = transform.position.z + cameraOffsetZ;
        mainCamera.transform.position = cameraPosition;
        Debug.Log($"Camera updated: {cameraPosition.z}");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter called with: {other.tag}");
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle encountered! Stopping forward movement.");
            shouldStopForward = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"OnCollisionEnter called with: {collision.gameObject.tag}");
        // Check if cube has landed back on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Landed on ground.");
            isGrounded = true;
        }
    }
}

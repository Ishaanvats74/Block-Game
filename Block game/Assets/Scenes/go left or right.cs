using UnityEngine;

public class MoveCubeInput : MonoBehaviour
{
    public float speed = 5.0f; // Adjust this value to change the speed

    void Update()
    {
        // Get input from the horizontal axis (left and right arrow keys or A and D keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move the object left and right based on the input
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
    }
}

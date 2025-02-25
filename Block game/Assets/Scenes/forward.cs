using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public float speed = 5.0f;
    private bool shouldStop = false;

    void Update()
    {
        if (!shouldStop)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            shouldStop = true;
        }
    }
}

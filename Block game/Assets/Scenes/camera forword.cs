using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public GameObject cube;
    public float followSpeed = 5.0f;
    private bool shouldStop = false;

    void Update()
    {
        if (cube == null)
        {
            Debug.LogError("Cube is not assigned in the Inspector!");
            return;
        }

        if (!shouldStop)
        {
            Vector3 newPosition = transform.position;
            newPosition.z = Mathf.Lerp(transform.position.z, cube.transform.position.z, Time.deltaTime * followSpeed);
            transform.position = newPosition;
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

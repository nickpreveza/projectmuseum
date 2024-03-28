using UnityEngine;

public class AutoSpinner : MonoBehaviour
{
    public float speed = 1.0f; // Speed of rotation

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around all axes
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
        transform.Rotate(Vector3.right, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }

    // Public function to set the speed of rotation
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
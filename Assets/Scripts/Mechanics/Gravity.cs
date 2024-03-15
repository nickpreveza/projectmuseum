using UnityEngine;

public class Gravity : MonoBehaviour
{

    public Rigidbody rb;
    public GameObject cube;
    public ConstantForce gravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gravity = GetComponent<ConstantForce>();
        rb = GetComponent<Rigidbody>();
        gravity.force = new Vector3(-9.81f, 9.81f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

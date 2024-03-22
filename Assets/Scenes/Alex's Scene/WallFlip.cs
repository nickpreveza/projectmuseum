using UnityEngine;

public class WallFlip : MonoBehaviour
{
    public float wallWalkSpeed = 5f; // Speed of movement on walls
    public float jumpForce = 10f; // Force applied when jumping off walls
    public LayerMask wallLayer; // Layer mask for walls

    private Rigidbody rb;
    private bool isWalkingOnWall = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isWalkingOnWall)
        {
            // Jump off the wall
            Vector3 jumpDirection = Vector3.up + transform.forward;
            rb.linearVelocity = jumpDirection * jumpForce;
            isWalkingOnWall = false;
        }
    }

    void FixedUpdate()
    {
        // Cast a ray to check if the player is touching a wall
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f, wallLayer))
        {
            Debug.Log("yes");
            // Align player with the wall and enable wall walking
            isWalkingOnWall = true;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * 10f);
            rb.linearVelocity = transform.forward * wallWalkSpeed;
        }
        else
        {
            // Disable wall walking if not touching the wall
            isWalkingOnWall = false;
        }
    }
}

using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();


        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        animator.SetFloat("Speed", movement.magnitude);

        if (movement.magnitude > 0.1f)
        {
            Vector3 targetVelocity = movement * moveSpeed;

            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

            rb.MoveRotation(Quaternion.LookRotation(movement));
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }
}
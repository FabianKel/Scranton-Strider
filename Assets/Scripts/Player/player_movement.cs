using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Bloquea la rotación en X y Z para que Michael no se caiga como un tronco
        // cuando choque con un escritorio.
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
            // SOLUCIÓN: En lugar de calcular posición, aplicamos velocidad lineal
            Vector3 targetVelocity = movement * moveSpeed;

            // Mantenemos la velocidad en Y por si hay gravedad o saltos
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);

            // Rotación suave (opcional, pero se ve mejor)
            rb.MoveRotation(Quaternion.LookRotation(movement));
        }
        else
        {
            // Si no hay input, detenemos el movimiento horizontal de inmediato
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }
}
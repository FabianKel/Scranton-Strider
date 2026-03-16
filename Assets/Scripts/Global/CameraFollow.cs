using UnityEngine;

public sealed class CameraFollow : MonoBehaviour
{
    [Header("Configuración de Seguimiento")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float smoothSpeed = 0.125f;

    private Transform target;
    private Vector3 offset;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            target = player.transform;

            transform.position = new Vector3(-35, 40, 15);
            transform.rotation = Quaternion.Euler(60f, 0f, 0f);

            offset = transform.position - target.position;
        }
        else
        {
            Debug.LogError($"No se encontró ningún objeto con el tag '{playerTag}'");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
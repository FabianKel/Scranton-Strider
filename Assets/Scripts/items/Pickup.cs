using UnityEngine;

public abstract class Pickup<T> : MonoBehaviour where T : PickupData
{
    [SerializeField] protected T data;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (data.pickupSound != null && AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX(data.pickupSound);

        Use();
        Destroy(gameObject);
    }

    protected abstract void Use();
}
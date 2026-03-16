using System;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [Header("General Info")]
    public string ItemName;
    public string Description;
    public string Type;
    public Sprite ItemIcon;

    [SerializeField] public AudioClip pickupSound;

    public static event Action<Pickup> OnPickupCollected;

    public void Collect()
    {
        Use();
        //OnPickupCollected.Invoke(this);
        Destroy(gameObject);

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    protected abstract void Use();



}
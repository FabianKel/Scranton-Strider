using System;
using UnityEngine;

public class Item : Pickup
{

    [Header("Stats")]
    public int Speed;

    protected override void Use()
    {
        Guardar();
    }

    private void Guardar()
    {

        if (EventManager.Instance != null)
        {
            EventManager.Instance.TriggerItemTaken(ItemName, Speed);
            FindAnyObjectByType<LevelManager>().SaveOnInventory(ItemName, ItemIcon);

            Debug.Log($"Has recogido {ItemName}: {Description}.");
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(pickupSound);
        }
    }
}
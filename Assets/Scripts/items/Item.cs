using System;
using UnityEngine;

public class Item : Pickup<ItemData>
{
    protected override void Use()
    {
        EventManager.Instance?.TriggerItemTaken(data.ItemName, data.Speed);
        FindAnyObjectByType<LevelManager>().SaveOnInventory(data.ItemName, data.ItemIcon);

        Debug.Log($"Has recogido {data.ItemName}: {data.Description}.");
    }
}
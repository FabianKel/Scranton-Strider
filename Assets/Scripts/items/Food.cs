using System;
using UnityEngine;

public class Food : Pickup<FoodData>
{
    protected override void Use()
    {
        EventManager.Instance?.TriggerFoodEaten(data.carbs, data.protein, data.fat, data.hydration);
        FindAnyObjectByType<LevelManager>().IncreaseStats(data.carbs, data.protein, data.fat, data.hydration);

        Debug.Log($"Consumiste {data.ItemName}. Ganaste Carbs: {data.carbs}");
    }
}
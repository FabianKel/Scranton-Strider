using System;
using UnityEngine;

public class Food : Pickup
{

    [Header("Nutrición")]
    public int carbs;
    public int protein;
    public int fat;
    public int hydration;

    protected override void Use()
    {
        Comer();
    }

    private void Comer()
    {

        if (EventManager.Instance != null)
        {
            EventManager.Instance.TriggerFoodEaten(carbs, protein, fat, hydration);
            FindAnyObjectByType<LevelManager>().IncreaseStats(carbs, protein, fat, hydration);

            Debug.Log($"Ganaste carbs: {carbs}, protein: {protein}, fat: {fat}, hydration: {hydration}.");
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(pickupSound);
        }
    }
}
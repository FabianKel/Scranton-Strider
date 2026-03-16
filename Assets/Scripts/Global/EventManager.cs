using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public event Action<int, int, int, int> OnFoodEaten;
    public event Action<string, int> OnItemTaken;

    public void TriggerFoodEaten(int carbs, int protein, int fat, int hydration)
    {
        OnFoodEaten?.Invoke(carbs, protein, fat, hydration);
    }

    public void TriggerItemTaken(string itemName, int speed)
    {
        OnItemTaken?.Invoke(itemName, speed);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}

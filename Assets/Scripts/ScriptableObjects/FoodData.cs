using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "Inventory/Food")]
public class FoodData : PickupData
{
    public int carbs;
    public int protein;
    public int fat;
    public int hydration;
}
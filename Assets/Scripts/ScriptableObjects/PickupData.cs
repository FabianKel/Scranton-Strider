using UnityEngine;

public abstract class PickupData : ScriptableObject
{
    [Header("General Info")]
    public string ItemName;
    [TextArea] public string Description;
    public Sprite ItemIcon;
    public AudioClip pickupSound;
}
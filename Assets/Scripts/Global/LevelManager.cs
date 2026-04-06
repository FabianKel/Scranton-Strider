using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] float timeLimit = 30f;
    [SerializeField] int gearNeededCount = 3;
    [SerializeField] int maxStatValue = 100;

    private List<string> inventoryItems = new List<string>();
    private int totalCarbs, totalProtein, totalFat, totalHydration;
    private float currentTime;
    private bool gameActive = false;

    private player_movement playerMovement;

    void Start()
    {
        currentTime = timeLimit;
        playerMovement = FindAnyObjectByType<player_movement>();

        // Inicializar UI
        GameUIManager.Instance.UpdateStatsUI(0, 0, 0, 0);
        GameUIManager.Instance.UpdateTimerUI(currentTime);
    }

    void Update()
    {
        if (gameActive)
        {
            UpdateTimer();
        }
    }

    public void StartLevel()
    {
        gameActive = true;
        GameUIManager.Instance.StartGame();
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        GameUIManager.Instance.UpdateTimerUI(currentTime);

        if (currentTime <= 0)
        {
            currentTime = 0;
            EndOfficePhase();
        }
    }

    public void SaveOnInventory(string itemName, Sprite itemIcon)
    {
        if (!gameActive) return;

        inventoryItems.Add(itemName);
        GameUIManager.Instance.UpdateInventoryUI(inventoryItems.Count - 1, itemIcon);
    }

    public void IncreaseStats(int carbs, int protein, int fat, int hydration)
    {
        if (!gameActive) return;

        totalCarbs += carbs;
        totalProtein += protein;
        totalFat += fat;
        totalHydration += hydration;

        GameUIManager.Instance.UpdateStatsUI(totalCarbs, totalProtein, totalFat, totalHydration);
    }

    private void EndOfficePhase()
    {
        gameActive = false;
        if (playerMovement != null) playerMovement.enabled = false;

        RunManager.Instance.SimulateRace(
            totalCarbs, totalProtein, totalFat, totalHydration,
            inventoryItems, gearNeededCount
        );
    }
}
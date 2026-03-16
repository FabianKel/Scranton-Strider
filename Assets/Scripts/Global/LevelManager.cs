using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Stats UI")]
    [SerializeField] TextMeshProUGUI CarbsText;
    [SerializeField] TextMeshProUGUI ProteinText;
    [SerializeField] TextMeshProUGUI FatText;
    [SerializeField] TextMeshProUGUI HydrationText;

    [Header("Game Flow UI")]
    [SerializeField] TextMeshProUGUI TimerText;
    [SerializeField] TextMeshProUGUI ResultText;

    [Header("Inventory UI Slots")]
    [SerializeField] Image slot1Image;
    [SerializeField] Image slot2Image;
    [SerializeField] Image slot3Image;

    [Header("Configuración")]
    [SerializeField] float timeLimit = 30f;
    [SerializeField] int gearNeededCount = 3;
    [SerializeField] int maxStatValue = 70;

    private List<string> inventoryItems = new List<string>();
    private int totalCarbs, totalProtein, totalFat, totalHydration;
    private float currentTime;
    private bool gameActive = true;

    void Start()
    {
        currentTime = timeLimit;
        ResultText.text = "";
    }

    void Update()
    {
        if (gameActive)
        {
            UpdateTimer();
        }
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        TimerText.text = $"{Mathf.Ceil(currentTime)}s";

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
        UpdateInventoryUI(itemIcon);
    }

    public void IncreaseStats(int carbs, int protein, int fat, int hydration)
    {
        if (!gameActive) return;
        totalCarbs += carbs;
        totalProtein += protein;
        totalFat += fat;
        totalHydration += hydration;

        CarbsText.text = $"Carbs: {totalCarbs}";
        ProteinText.text = $"Protein: {totalProtein}";
        FatText.text = $"Fat: {totalFat}";
        HydrationText.text = $"Hydration: {totalHydration}";
    }

    private void EndOfficePhase()
    {
        gameActive = false;
        FindAnyObjectByType<player_movement>().enabled = false;

        SimulateRace();
    }

    private void SimulateRace()
    {
        string finalMessage = "";

        if (inventoryItems.Count < gearNeededCount)
        {
            finalMessage = "Michael no pudo participar por no tener el equipo completo (Kit, Gorra, Tennis).";
        }
        else if (totalHydration < maxStatValue)
        {
            finalMessage = "Michael se desmayó por deshidratación a mitad de la carrera. ¡El agua es importante!";
        }
        else if (totalCarbs > maxStatValue || totalFat > maxStatValue)
        {
            finalMessage = "Michael vomitó en el asfalto. ¡Demasiado Fettuccine Alfredo antes de correr!";
        }
        else if (totalProtein < 40)
        {
            finalMessage = "Sus piernas no aguantaron. Michael se quedó tirado a pocos metros de la meta.";
        }

        else if (totalCarbs >= 50 && totalProtein >= 50)
        {
            finalMessage = "¡INCREÍBLE! Michael terminó la carrera. No ganó, pero terminó.";
        }
        else
        {
            finalMessage = "Michael terminó la carrera caminando... le faltó energía.";
        }

        ResultText.text = finalMessage;
        Debug.Log("RESULTADO: " + finalMessage);
    }

    private void UpdateInventoryUI(Sprite newIcon)
    {
        if (inventoryItems.Count == 1) { slot1Image.sprite = newIcon; slot1Image.enabled = true; }
        else if (inventoryItems.Count == 2) { slot2Image.sprite = newIcon; slot2Image.enabled = true; }
        else if (inventoryItems.Count == 3) { slot3Image.sprite = newIcon; slot3Image.enabled = true; }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public partial class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }

    private static bool shouldAutoStart = false;

    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject gameHUD;
    [SerializeField] private GameObject runsScreen;
    [SerializeField] private GameObject endScreen;

    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI carbsText;
    [SerializeField] private TextMeshProUGUI proteinText;
    [SerializeField] private TextMeshProUGUI fatText;
    [SerializeField] private TextMeshProUGUI hydrationText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Inventory Slots")]
    [SerializeField] private Image[] inventorySlots;

    [Header("End Screen Elements")]
    [SerializeField] private TextMeshProUGUI resultText;

    [Header("Special Screens")]
    [SerializeField] private RunsScreenController runsScreenController;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (shouldAutoStart)
        {
            shouldAutoStart = false;
            StartGameFromRestart();
        }
        else
        {
            ShowMainMenu();
        }
    }

    // --- NAVEGACIÓN ---
    public void ShowMainMenu()
    {
        Time.timeScale = 0f;
        SwitchPanel(mainMenuPanel);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SwitchPanel(gameHUD);
    }

    public void OpenSettings() => SwitchPanel(settingsPanel);
    public void OpenRunsScreen()
    {
        SwitchPanel(runsScreen);
        if (runsScreenController != null)
        {
            runsScreenController.ShowEndings();
        }
    }

    public void ResetInventoryUI()
    {
        foreach (var slot in inventorySlots)
        {
            slot.sprite = null;
            slot.enabled = false;
        }
    }

    public void ShowEndScreen(string message)
    {
        resultText.text = message;
        SwitchPanel(endScreen);
    }

    private void SwitchPanel(GameObject panelToShow)
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        gameHUD.SetActive(false);
        runsScreen.SetActive(false);
        endScreen.SetActive(false);

        if (panelToShow != null) panelToShow.SetActive(true);
    }

    // --- ACTUALIZACIÓN DE UI ---
    public void UpdateStatsUI(int c, int p, int f, int h)
    {
        carbsText.text = $"Carbs: {c}";
        proteinText.text = $"Protein: {p}";
        fatText.text = $"Fat: {f}";
        hydrationText.text = $"Hydration: {h}";
    }

    public void UpdateTimerUI(float time)
    {
        timerText.text = $"{Mathf.Ceil(time)}s";
    }

    public void UpdateInventoryUI(int slotIndex, Sprite icon)
    {
        if (slotIndex < inventorySlots.Length)
        {
            inventorySlots[slotIndex].sprite = icon;
            inventorySlots[slotIndex].enabled = true;
        }
    }

    public void RestartLevel()
    {
        shouldAutoStart = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void StartGameFromRestart()
    {
        LevelManager lm = FindAnyObjectByType<LevelManager>();
        if (lm != null)
        {
            lm.StartLevel();
        }
    }

    public void QuitGame() => Application.Quit();

    public void GoToMainMenu()
    {
        shouldAutoStart = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
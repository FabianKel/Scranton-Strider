using UnityEngine;
using System.Collections.Generic;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance { get; private set; }

    private void Awake() => Instance = this;

    public void SimulateRace(int carbs, int protein, int fat, int hydration,  List<string> inventoryItems, int gearNeeded)
    {
        Result currentRun;

        if (inventoryItems.Count < gearNeeded)
            currentRun = CreateResult("NO_GEAR", carbs, protein, fat, hydration, inventoryItems);
        else if (hydration < 70)
            currentRun = CreateResult("DEHYDRATED", carbs, protein, fat, hydration, inventoryItems);
        else if (carbs >= 100 || fat >= 100)
            currentRun = CreateResult("VOMIT", carbs, protein, fat, hydration, inventoryItems);
        else if (protein < 50)
            currentRun = CreateResult("NO_PROTEIN", carbs, protein, fat, hydration, inventoryItems);
        else if (carbs >= 70 && protein >= 60)
            currentRun = CreateResult("WIN", carbs, protein, fat, hydration, inventoryItems);
        else
            currentRun = CreateResult("WALK", carbs, protein, fat, hydration, inventoryItems);

        // Guardar persistencia
        PersistenceManager.Instance.UnlockEnding(currentRun.ID);
        PersistenceManager.Instance.SaveRunToHistory(currentRun);

        GameUIManager.Instance.ShowEndScreen($"{currentRun.Title}\n{currentRun.Reason}");
    }

    private Result CreateResult(string id, int c, int p, int f, int h, List<string> inventoryItems)
    {

        List<Result> templates = PersistenceManager.Instance.LoadEndings();
        Result template = templates.Find(t => t.ID == id);

        Result run = new Result(id, template.Title, template.Description, template.Reason);
        run.hasKit = inventoryItems.Contains("Runner Kit");
        run.hasVisor = inventoryItems.Contains("Cap");
        run.hasShoes = inventoryItems.Contains("Shoes");
        run.totalCarbs = c;
        run.totalProtein = p;
        run.totalFat = f;
        run.totalHydration = h;
        return run;
    }
}
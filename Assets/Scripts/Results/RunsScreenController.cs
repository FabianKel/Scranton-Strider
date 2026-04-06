using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunsScreenController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Transform container;
    [SerializeField] GameObject entryPrefab;
    [SerializeField] TextMeshProUGUI screenTitle;

    private void OnEnable()
    {
        ShowEndings();
    }

    public void ShowEndings()
    {
        screenTitle.text = "POSSIBLE ENDINGS";
        CleanContainer();
        List<Result> endings = PersistenceManager.Instance.LoadEndings();

        foreach (var res in endings)
        {
            InstantiateEntry(res, false);
        }
    }

    public void ShowHistory()
    {
        screenTitle.text = "RUN HISTORY";
        CleanContainer();
        List<Result> history = PersistenceManager.Instance.LoadHistory();

        history.Reverse();

        foreach (var res in history)
        {
            InstantiateEntry(res, true);
        }
    }

    private void InstantiateEntry(Result data, bool isHistory)
    {
        GameObject go = Instantiate(entryPrefab, container);
        go.GetComponent<ResultEntry>().Setup(data, isHistory);
    }

    private void CleanContainer()
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
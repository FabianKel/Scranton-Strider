using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance { get; private set; }

    private string historyPath;
    private string resultsPath;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        historyPath = Path.Combine(Application.persistentDataPath, "history.json");
        resultsPath = Path.Combine(Application.persistentDataPath, "Results.json");
    }

    public void SaveRunToHistory(Result runData)
    {
        List<Result> history = LoadHistory();
        history.Add(runData);
        File.WriteAllText(historyPath, JsonHelper.ToJson(history.ToArray(), true));
    }

    public List<Result> LoadHistory()
    {
        if (!File.Exists(historyPath)) return new List<Result>();
        string json = File.ReadAllText(historyPath);
        return new List<Result>(JsonHelper.FromJson<Result>(json));
    }

    public void UnlockEnding(string id)
    {
        List<Result> endings = LoadEndings();
        var ending = endings.Find(e => e.ID == id);
        if (ending != null && ending.Locked)
        {
            ending.Locked = false;
            File.WriteAllText(resultsPath, JsonHelper.ToJson(endings.ToArray(), true));
        }
    }

    public List<Result> LoadEndings()
    {
        if (!File.Exists(resultsPath)) return InitializeEndings();
        return new List<Result>(JsonHelper.FromJson<Result>(File.ReadAllText(resultsPath)));
    }

    private List<Result> InitializeEndings()
    {
        List<Result> defaults = new List<Result>
        {
            new Result("NO_GEAR", "The Dundie Failure", "Michael didn't even have running shoes.", "Lack of basic equipment."),
            new Result("DEHYDRATED", "The Desert Spirit", "Michael collapsed. Water is life!", "Critical dehydration."),
            new Result("VOMIT", "Fettuccine Incident", "BLARGH! Too much Alfredo sauce...", "Stomach overload."),
            new Result("NO_PROTEIN", "Weak Knees", "His legs gave out like a folding chair.", "Insufficient protein."),
            new Result("WIN", "The Rabies Run", "He finished! Not first, but he finished.", "Optimal nutrition."),
            new Result("WALK", "The Lazy Walk", "He basically walked it. Just like a Monday morning.", "Low energy.")
        };
        File.WriteAllText(resultsPath, JsonHelper.ToJson(defaults.ToArray(), true));
        return defaults;
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }
    public static string ToJson<T>(T[] array, bool pretty)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, pretty);
    }
    [Serializable] private class Wrapper<T> { public T[] Items; }
}
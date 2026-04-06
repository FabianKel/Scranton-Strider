using System;

[Serializable]
public class Result
{
    public string ID;
    public string Title;
    public string Description;
    public string Reason;
    public bool Locked = true;

    public int totalCarbs;
    public int totalProtein;
    public int totalFat;
    public int totalHydration;
    public bool hasKit;
    public bool hasVisor;
    public bool hasShoes;

    public Result(string id, string title, string desc, string reason)
    {
        ID = id;
        Title = title;
        Description = desc;
        Reason = reason;
    }
}
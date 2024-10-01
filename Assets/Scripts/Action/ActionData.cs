using System;

public class ActionData
{
    public string actionName;
    public int timeUnitsCost;
    public Action<Unit> executeAction;

    public ActionData(string name, int cost, Action<Unit> action)
    {
        actionName = name;
        timeUnitsCost = cost;
        executeAction = action;
    }
}

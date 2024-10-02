using UnityEngine;

public static class Actions
{
    public static ActionData Wait = new ActionData("Wait", 1, (unit) =>
    {
        Debug.Log($"{unit.unitData.name} is waiting.");
    });

    public static ActionData Attack = new ActionData("Attack", 15, (unit) =>
    {
        Debug.Log($"{unit.unitData.name} performs an attack.");
    });

    public static ActionData Move = new ActionData("Move", 5, (unit) =>
    {
        if (unit.unitData.isPlayerControlled)
        {
            unit.StartCoroutine(unit.PlayerMoveAction());
        }
    });
}

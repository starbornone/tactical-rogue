using UnityEngine;

public static class Actions
{
  public static ActionData Wait = new ActionData("Wait", 10, (unit) =>
  {
    Debug.Log($"{unit.unitData.name} is waiting.");
  });

  public static ActionData Attack = new ActionData("Attack", 15, (unit) =>
  {
    Debug.Log($"{unit.unitData.name} performs an attack.");
  });
}

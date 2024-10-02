using System.Collections;
using UnityEngine;

public class HealWeakestAllyBehavior : IUnitBehavior
{
    public int Priority => 8;

    public bool IsApplicable(Unit unit)
    {
        return FindWeakestAlly(unit) != null && unit.HasHealingItem();
    }

    public IEnumerator Execute(Unit unit)
    {
        Unit target = FindWeakestAlly(unit);
        if (target != null)
        {
            yield return unit.MoveToPosition(target.unitData.map.x, target.unitData.map.y);
            unit.UseHealingItemOn(target);
        }
    }

    private Unit FindWeakestAlly(Unit unit)
    {
        Unit[] allUnits = Object.FindObjectsOfType<Unit>();
        Unit weakestAlly = null;
        int lowestHealth = int.MaxValue;

        foreach (Unit otherUnit in allUnits)
        {
            if (otherUnit.unitData.isPlayerControlled != unit.unitData.isPlayerControlled)
                continue;

            int health = otherUnit.unitData.attributes.stamina;

            if (health < lowestHealth)
            {
                lowestHealth = health;
                weakestAlly = otherUnit;
            }
        }

        return weakestAlly;
    }
}

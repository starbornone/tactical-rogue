using System.Collections;
using UnityEngine;

public class AttackNearestEnemyBehavior : IUnitBehavior
{
    public int Priority => 10;

    public bool IsApplicable(Unit unit)
    {
        return FindNearestEnemy(unit) != null;
    }

    public IEnumerator Execute(Unit unit)
    {
        Unit target = FindNearestEnemy(unit);
        if (target != null)
        {
            yield return unit.MoveToPosition(target.unitData.map.x, target.unitData.map.y);
            yield return unit.ExecuteAction(Actions.Attack);
        }
    }

    private Unit FindNearestEnemy(Unit unit)
    {
        Unit[] allUnits = Object.FindObjectsOfType<Unit>();
        Unit nearestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (Unit otherUnit in allUnits)
        {
            if (otherUnit.unitData.isPlayerControlled == unit.unitData.isPlayerControlled)
                continue;

            float distance = Vector2Int.Distance(
                new Vector2Int(unit.unitData.map.x, unit.unitData.map.y),
                new Vector2Int(otherUnit.unitData.map.x, otherUnit.unitData.map.y));

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = otherUnit;
            }
        }

        return nearestEnemy;
    }
}

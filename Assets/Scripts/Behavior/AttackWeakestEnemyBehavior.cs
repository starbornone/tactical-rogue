using System.Collections;
using UnityEngine;

public class AttackWeakestEnemyBehavior : IUnitBehavior
{
    public int Priority => 9;

    public bool IsApplicable(Unit unit)
    {
        return FindWeakestEnemy(unit) != null;
    }

    public IEnumerator Execute(Unit unit)
    {
        Unit target = FindWeakestEnemy(unit);
        if (target != null)
        {
            yield return unit.MoveToPosition(target.unitData.map.x, target.unitData.map.y);
            yield return unit.ExecuteAction(Actions.Attack);
        }
    }

    private Unit FindWeakestEnemy(Unit unit)
    {
        Unit[] allUnits = Object.FindObjectsOfType<Unit>();
        Unit weakestEnemy = null;
        int lowestHealth = int.MaxValue;

        foreach (Unit otherUnit in allUnits)
        {
            if (otherUnit.unitData.isPlayerControlled == unit.unitData.isPlayerControlled)
                continue;

            int health = otherUnit.unitData.attributes.stamina; // Assuming stamina represents health

            if (health < lowestHealth)
            {
                lowestHealth = health;
                weakestEnemy = otherUnit;
            }
        }

        return weakestEnemy;
    }
}

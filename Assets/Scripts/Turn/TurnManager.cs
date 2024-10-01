using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    private Unit currentUnit;

    void Start()
    {
    }

    public void InitializeUnits()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();
        if (allUnits.Length == 0)
        {
            Debug.LogError("No units found in the scene.");
            return;
        }

        units.Clear();
        units.AddRange(allUnits);

        foreach (Unit unit in units)
        {
            unit.unitData.remainingTimeUnits = 0;
        }

        StartCoroutine(TurnLoop());
    }

    IEnumerator TurnLoop()
    {
        while (true)
        {
            if (units.Count == 0)
            {
                Debug.LogError("No units available to take turns.");
                yield break;
            }

            units.Sort((a, b) =>
            {
                int compare = a.unitData.remainingTimeUnits.CompareTo(b.unitData.remainingTimeUnits);
                if (compare == 0)
                {
                    compare = b.unitData.initiative.CompareTo(a.unitData.initiative);
                }
                return compare;
            });

            currentUnit = units[0];
            yield return StartCoroutine(UnitTurn(currentUnit));
        }
    }

    IEnumerator UnitTurn(Unit unit)
    {
        yield return unit.PerformAction();
        unit.unitData.remainingTimeUnits += unit.lastActionTimeUnitsCost;
    }
}

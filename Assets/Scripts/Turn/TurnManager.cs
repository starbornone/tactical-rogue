using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    private Unit currentUnit;

    private bool gameStarted = false;

    void Start()
    {
        UnitManager.OnUnitsSpawned += OnUnitsReady;
    }

    void OnDestroy()
    {
        UnitManager.OnUnitsSpawned -= OnUnitsReady;
    }

    void OnUnitsReady()
    {
        Debug.Log("Units are ready, you can start the game now.");
    }

    public void InitializeUnits()
    {
        Unit[] allUnits = FindObjectsOfType<Unit>();
        units.AddRange(allUnits);

        foreach (Unit unit in units)
        {
            unit.unitData.remainingTimeUnits = 0;
        }

        if (units.Count > 0)
        {
            StartCoroutine(TurnLoop());
        }
        else
        {
            Debug.LogError("No units available to take turns.");
        }
    }

    IEnumerator TurnLoop()
    {
        while (gameStarted)
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

    public void StartGame()
    {
        gameStarted = true;
        InitializeUnits();
    }
}

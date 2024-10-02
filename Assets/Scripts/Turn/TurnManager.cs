using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    private Unit currentUnit;

    private bool gameStarted = false;

    public static TurnManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
            unit.unitData.timeUnits.remaining = unit.unitData.timeUnits.maximum;
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

            bool allUnitsDepleted = true;
            foreach (Unit unit in units)
            {
                if (unit.unitData.timeUnits.remaining > 0)
                {
                    allUnitsDepleted = false;
                    break;
                }
            }

            if (allUnitsDepleted)
            {
                foreach (Unit unit in units)
                {
                    unit.unitData.timeUnits.remaining = unit.unitData.timeUnits.maximum;
                }
                continue;
            }

            units.Sort((a, b) =>
            {
                return b.unitData.timeUnits.remaining.CompareTo(a.unitData.timeUnits.remaining);
            });

            currentUnit = units[0];

            if (currentUnit.unitData.timeUnits.remaining <= 0)
            {
                continue;
            }

            yield return StartCoroutine(UnitTurn(currentUnit));
        }
    }

    IEnumerator UnitTurn(Unit unit)
    {
        yield return unit.PerformAction();
    }

    public void StartGame()
    {
        gameStarted = true;
        InitializeUnits();
    }
}

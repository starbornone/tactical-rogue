using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject unitPrefab;

    void Start()
    {
        GridMapGenerator mapGenerator = FindObjectOfType<GridMapGenerator>();
        mapGenerator.OnMapGenerated += SpawnUnits;
    }

    void SpawnUnits()
    {
        GridMapGenerator mapGenerator = FindObjectOfType<GridMapGenerator>();

        List<UnitData> unitsData = UnitsData.allUnits;

        foreach (UnitData data in unitsData)
        {
            int x = data.map.x;
            int z = data.map.y;
            int tileHeight = mapGenerator.GetTileHeight(x, z);
            Vector3 position = new Vector3(x, tileHeight + 1, z);
            GameObject unitObject = Instantiate(unitPrefab, position, Quaternion.identity);
            Unit unit = unitObject.GetComponent<Unit>();
            unit.unitData = data;
        }

        TurnManager turnManager = FindObjectOfType<TurnManager>();
        if (turnManager != null)
        {
            turnManager.InitializeUnits();
        }
        else
        {
            Debug.LogError("TurnManager not found in the scene.");
        }
    }
}

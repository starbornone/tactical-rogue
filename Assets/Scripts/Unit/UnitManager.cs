using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GameObject unitPrefab;

    public static event System.Action OnUnitsSpawned;

    void Start()
    {
        GridMapGenerator mapGenerator = FindObjectOfType<GridMapGenerator>();
        if (mapGenerator != null)
        {
            mapGenerator.OnMapGenerated += SpawnUnits;
        }
        else
        {
            Debug.LogError("GridMapGenerator not found. Make sure it's in the scene.");
        }
    }

    void SpawnUnits()
    {
        GridMapGenerator mapGenerator = FindObjectOfType<GridMapGenerator>();
        if (mapGenerator == null)
        {
            Debug.LogError("Failed to find GridMapGenerator for unit spawning.");
            return;
        }

        List<UnitData> unitsData = UnitsData.allUnits;
        foreach (UnitData data in unitsData)
        {
            int x = data.map.x;
            int z = data.map.y;

            int tileHeight = mapGenerator.GetTileHeight(x, z);
            Vector3 position = new Vector3(x, tileHeight + 1, z);

            GameObject unitObject = Instantiate(unitPrefab, position, Quaternion.identity);
            Unit unit = unitObject.GetComponent<Unit>();
            if (unit != null)
            {
                unit.unitData = data;
            }
            else
            {
                Debug.LogError("Failed to find Unit component on unitPrefab.");
            }

            Debug.Log($"Unit {data.name} spawned at position {position}");
        }

        OnUnitsSpawned?.Invoke();
    }
}

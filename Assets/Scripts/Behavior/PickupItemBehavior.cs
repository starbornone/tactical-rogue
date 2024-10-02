using System.Collections;
using UnityEngine;

public class PickupItemBehavior : IUnitBehavior
{
    public int Priority => 5;

    public bool IsApplicable(Unit unit)
    {
        return !EnemiesNearby(unit) && FindNearestItem(unit) != null;
    }

    public IEnumerator Execute(Unit unit)
    {
        ItemOnMap nearestItem = FindNearestItem(unit);
        if (nearestItem != null)
        {
            Vector2Int itemPosition = new Vector2Int(nearestItem.mapPosition.x, nearestItem.mapPosition.y);
            Item itemData = nearestItem.itemData;
            GameObject itemObject = nearestItem.gameObject;

            yield return unit.MoveToPosition(itemPosition.x, itemPosition.y);

            unit.unitData.carriedItems.Add(itemData);

            Object.Destroy(itemObject);
        }
    }

    private ItemOnMap FindNearestItem(Unit unit)
    {
        ItemOnMap[] allItems = Object.FindObjectsOfType<ItemOnMap>();
        ItemOnMap nearestItem = null;
        float minDistance = float.MaxValue;

        foreach (ItemOnMap item in allItems)
        {
            float distance = Vector2Int.Distance(
                new Vector2Int(unit.unitData.map.x, unit.unitData.map.y),
                new Vector2Int(item.mapPosition.x, item.mapPosition.y));

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestItem = item;
            }
        }

        return nearestItem;
    }

    private bool EnemiesNearby(Unit unit)
    {
        Unit[] allUnits = Object.FindObjectsOfType<Unit>();
        foreach (Unit otherUnit in allUnits)
        {
            if (otherUnit.unitData.isPlayerControlled == unit.unitData.isPlayerControlled)
                continue;

            float distance = Vector2Int.Distance(
                new Vector2Int(unit.unitData.map.x, unit.unitData.map.y),
                new Vector2Int(otherUnit.unitData.map.x, otherUnit.unitData.map.y));

            if (distance <= 5)
                return true;
        }
        return false;
    }
}

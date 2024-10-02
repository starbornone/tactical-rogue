using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;

    void Start()
    {
        GridMapGenerator mapGenerator = FindObjectOfType<GridMapGenerator>();
        mapGenerator.OnMapGenerated += SpawnItems;
    }

    void SpawnItems()
    {
        GridMapGenerator mapGenerator = FindObjectOfType<GridMapGenerator>();
        List<Item> itemsToSpawn = new List<Item>
        {
            ItemsData.healthPotion,
            ItemsData.healthPotion,
            ItemsData.healthPotion,
            ItemsData.healthPotion
        };

        List<Vector2Int> occupiedPositions = new List<Vector2Int>();
        Unit[] units = FindObjectsOfType<Unit>();
        foreach (Unit unit in units)
        {
            occupiedPositions.Add(new Vector2Int(unit.unitData.map.x, unit.unitData.map.y));
        }

        ItemOnMap[] existingItems = FindObjectsOfType<ItemOnMap>();
        foreach (ItemOnMap item in existingItems)
        {
            occupiedPositions.Add(new Vector2Int(item.mapPosition.x, item.mapPosition.y));
        }

        List<Vector2Int> availablePositions = new List<Vector2Int>();
        for (int x = 0; x < mapGenerator.mapWidth; x++)
        {
            for (int z = 0; z < mapGenerator.mapLength; z++)
            {
                Vector2Int pos = new Vector2Int(x, z);
                if (!occupiedPositions.Contains(pos))
                {
                    availablePositions.Add(pos);
                }
            }
        }

        System.Random random = new System.Random();
        foreach (Item itemData in itemsToSpawn)
        {
            if (availablePositions.Count == 0)
                break;

            int index = random.Next(availablePositions.Count);
            Vector2Int position = availablePositions[index];
            availablePositions.RemoveAt(index);
            occupiedPositions.Add(position);

            int tileHeight = mapGenerator.GetTileHeight(position.x, position.y);
            Vector3 spawnPosition = new Vector3(position.x, tileHeight + 1, position.y);

            GameObject itemObject = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            ItemOnMap itemOnMap = itemObject.GetComponent<ItemOnMap>();
            itemOnMap.itemData = itemData;
            itemOnMap.mapPosition = new MapPosition { x = position.x, y = position.y };
        }
    }
}

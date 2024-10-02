using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public GameObject tilePrefab;
    public int width;
    public int height;

    public Node[,] grid;
    private Unit movingUnit;
    public bool destinationSelected = false;
    private List<Node> path;

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
        GridMapGenerator mapGenerator = FindObjectOfType<GridMapGenerator>();
        if (mapGenerator != null)
        {
            mapGenerator.OnMapGenerated += InitializeGrid;
        }
        else
        {
            Debug.LogError("GridMapGenerator not found. Make sure it's in the scene.");
        }
    }

    void InitializeGrid()
    {
        GridMapGenerator mapGenerator = FindObjectOfType<GridMapGenerator>();
        if (mapGenerator == null)
        {
            Debug.LogError("GridMapGenerator not found during grid initialization.");
            return;
        }

        width = mapGenerator.mapWidth;
        height = mapGenerator.mapLength;

        grid = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int tileHeight = mapGenerator.GetTileHeight(x, y);
                bool walkable = true;
                grid[x, y] = new Node(x, y, tileHeight, walkable);
            }
        }

        Debug.Log("Grid initialized after map generation.");
    }

    public void SetUnitForMovement(Unit unit)
    {
        movingUnit = unit;
        destinationSelected = false;
    }

    public void OnTileClicked(int x, int y)
    {
        if (movingUnit != null)
        {
            Node startNode = grid[movingUnit.unitData.map.x, movingUnit.unitData.map.y];
            Node targetNode = grid[x, y];

            path = Pathfinding.FindPath(grid, startNode, targetNode);
            destinationSelected = true;
        }
    }

    public List<Node> GetPath()
    {
        return path;
    }

    public void ResetMovement()
    {
        movingUnit = null;
        destinationSelected = false;
        path = null;
    }

    public Node GetNodeAtPosition(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
            return grid[x, y];
        return null;
    }

    public void SetNodeWalkable(int x, int y, bool walkable)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            if (grid[x, y] != null)
            {
                grid[x, y].walkable = walkable;
            }
            else
            {
                Debug.LogError($"Node at position ({x}, {y}) is null.");
            }
        }
    }
}

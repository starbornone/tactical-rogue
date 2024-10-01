using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public GameObject tilePrefab;
    public int width;
    public int height;

    private Node[,] grid;
    private Unit movingUnit;
    public bool destinationSelected = false;
    private List<Node> path;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeGrid();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeGrid()
    {
        width = 48;
        height = 48;
        grid = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new Node(x, y, true);
            }
        }
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
}

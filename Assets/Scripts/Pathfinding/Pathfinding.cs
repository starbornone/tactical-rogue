using UnityEngine;
using System.Collections.Generic;

public static class Pathfinding
{
    public static List<Node> FindPath(Node[,] grid, Node startNode, Node targetNode)
    {
        PriorityQueue<Node> openSet = new PriorityQueue<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Enqueue(startNode, 0);

        startNode.gCost = 0;
        startNode.hCost = GetHeuristic(startNode, targetNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.Dequeue();

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            closedSet.Add(currentNode);

            foreach (Node neighbor in GetNeighbors(grid, currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                float movementCost = GetMovementCost(currentNode, neighbor);
                float tentativeGCost = currentNode.gCost + movementCost;

                if (tentativeGCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = tentativeGCost;
                    neighbor.hCost = GetHeuristic(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Enqueue(neighbor, neighbor.fCost);
                    }
                }
            }
        }

        return null;
    }

    static List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    static List<Node> GetNeighbors(Node[,] grid, Node node)
    {
        List<Node> neighbors = new List<Node>();
        int gridWidth = grid.GetLength(0);
        int gridHeight = grid.GetLength(1);

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.x + x;
                int checkY = node.y + y;

                if (checkX >= 0 && checkX < gridWidth && checkY >= 0 && checkY < gridHeight)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    static float GetMovementCost(Node fromNode, Node toNode)
    {
        bool isDiagonal = (fromNode.x != toNode.x) && (fromNode.y != toNode.y);
        float baseCost = isDiagonal ? Mathf.Sqrt(2f) : 1f;
        float heightDifference = Mathf.Abs(toNode.height - fromNode.height);
        float heightCost = heightDifference;

        return baseCost + heightCost;
    }

    static float GetHeuristic(Node nodeA, Node nodeB)
    {
        float dx = Mathf.Abs(nodeA.x - nodeB.x);
        float dy = Mathf.Abs(nodeA.y - nodeB.y);
        float dz = Mathf.Abs(nodeA.height - nodeB.height);

        return Mathf.Sqrt(dx * dx + dy * dy) + dz;
    }

}

using UnityEngine;

public class Node
{
    public int x;
    public int y;
    public int height;
    public bool walkable;

    public float gCost;
    public float hCost;
    public Node parent;

    public float fCost
    {
        get { return gCost + hCost; }
    }

    public Node(int x, int y, int height, bool walkable)
    {
        this.x = x;
        this.y = y;
        this.height = height;
        this.walkable = walkable;
    }
}

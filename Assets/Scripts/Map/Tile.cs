using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;

    void OnMouseDown()
    {
        GridManager gridManager = GridManager.Instance;
        gridManager.OnTileClicked(x, y);
    }
}

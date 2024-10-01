using UnityEngine;

public class TileGenerator
{
  private GameObject tilePrefab;
  private Material topTileMaterial;
  private Material baseTileMaterial;

  public TileGenerator(GameObject tile, Material topMaterial, Material baseMaterial)
  {
    tilePrefab = tile;
    topTileMaterial = topMaterial;
    baseTileMaterial = baseMaterial;
  }

  public void GenerateTileColumn(int x, int z, int tileHeight, Transform parent)
  {
    for (int y = -12; y <= tileHeight; y++)
    {
      Vector3 position = new Vector3(x, y, z);
      GameObject tile = Object.Instantiate(tilePrefab, position, Quaternion.identity, parent);

      Renderer tileRenderer = tile.GetComponent<Renderer>();

      if (y == tileHeight)
      {
        tileRenderer.material = topTileMaterial;
      }
      else
      {
        tileRenderer.material = baseTileMaterial;
      }
    }
  }
}

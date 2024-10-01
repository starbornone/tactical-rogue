using UnityEngine;

public class GridMapGenerator : MonoBehaviour
{
    public int mapWidth = 48;
    public int mapLength = 48;
    public int minTileHeight = 0;
    public int maxTileHeight = 6;
    public GameObject tilePrefab;
    public Material topTileMaterial;
    public Material baseTileMaterial;

    private HeightMapGenerator heightMapGenerator;
    private TileGenerator tileGenerator;

    void Start()
    {
        heightMapGenerator = new HeightMapGenerator(mapWidth, mapLength, minTileHeight, maxTileHeight);
        tileGenerator = new TileGenerator(tilePrefab, topTileMaterial, baseTileMaterial);

        GenerateMap();
    }

    void GenerateMap()
    {
        float[,] heightMap = heightMapGenerator.GenerateHeightMap();

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapLength; z++)
            {
                int tileHeight = Mathf.RoundToInt(heightMap[x, z]);
                tileGenerator.GenerateTileColumn(x, z, tileHeight, transform);
            }
        }
    }
}

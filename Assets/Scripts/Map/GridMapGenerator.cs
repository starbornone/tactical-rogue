using UnityEngine;
using System;

public class GridMapGenerator : MonoBehaviour
{
    public int mapWidth = 48;
    public int mapLength = 48;
    public int minTileHeight = 0;
    public int maxTileHeight = 6;
    public float noiseScale = 20f;
    public int octaves = 4;
    [Range(0, 1)]
    public float persistence = 0.5f;
    public float lacunarity = 2f;
    public int seed = 42;
    public Vector2 offset;
    public GameObject tilePrefab;
    public Material topTileMaterial;
    public Material baseTileMaterial;

    public float[,] heightMap;

    public event Action OnMapGenerated;

    private HeightMapGenerator heightMapGenerator;

    void Start()
    {
        heightMapGenerator = new HeightMapGenerator(mapWidth, mapLength, minTileHeight, maxTileHeight)
        {
            noiseScale = noiseScale,
            octaves = octaves,
            persistence = persistence,
            lacunarity = lacunarity,
            seed = seed,
            offset = offset
        };

        GenerateMap();
    }

    void GenerateMap()
    {
        heightMap = heightMapGenerator.GenerateHeightMap();

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapLength; z++)
            {
                int tileHeight = Mathf.RoundToInt(heightMap[x, z]);

                for (int y = -12; y <= tileHeight; y++)
                {
                    Vector3 position = new Vector3(x, y, z);
                    GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                    Renderer tileRenderer = tile.GetComponent<Renderer>();

                    if (y == tileHeight)
                    {
                        tileRenderer.material = topTileMaterial;

                        Tile tileComponent = tile.AddComponent<Tile>();
                        tileComponent.x = x;
                        tileComponent.y = z;

                        if (!tile.TryGetComponent<Collider>(out _))
                        {
                            tile.AddComponent<BoxCollider>();
                        }
                    }
                    else
                    {
                        tileRenderer.material = baseTileMaterial;
                    }
                }
            }
        }

        OnMapGenerated?.Invoke();
    }

    public int GetTileHeight(int x, int z)
    {
        if (x >= 0 && x < mapWidth && z >= 0 && z < mapLength)
        {
            return Mathf.RoundToInt(heightMap[x, z]);
        }
        return 0;
    }
}

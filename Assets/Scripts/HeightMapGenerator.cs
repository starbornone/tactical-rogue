using UnityEngine;

public class HeightMapGenerator
{
    private int mapWidth;
    private int mapLength;
    private int minTileHeight;
    private int maxTileHeight;

    public float noiseScale = 20f;
    public int octaves = 4;
    public float persistence = 0.5f;
    public float lacunarity = 2f;
    public int seed = 42;
    public Vector2 offset;

    public HeightMapGenerator(int width, int length, int minHeight, int maxHeight)
    {
        mapWidth = width;
        mapLength = length;
        minTileHeight = minHeight;
        maxTileHeight = maxHeight;
    }

    public float[,] GenerateHeightMap()
    {
        float[,] heightMap = new float[mapWidth, mapLength];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (noiseScale <= 0)
        {
            noiseScale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfLength = mapLength / 2f;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapLength; y++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / noiseScale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfLength) / noiseScale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                heightMap[x, y] = noiseHeight;
            }
        }

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapLength; y++)
            {
                heightMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, heightMap[x, y]);
                heightMap[x, y] = heightMap[x, y] * (maxTileHeight - minTileHeight) + minTileHeight;
            }
        }

        return heightMap;
    }
}

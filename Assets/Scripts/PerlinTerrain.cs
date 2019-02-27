using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinTerrain : MonoBehaviour
{
    [Range(0, 255)]
    public int size = 50;
    [Range(0f, 100f)]
    public float gridSize = 1f;
    [Range(0f, 100f)]
    public float bumpiness = 10f;

    [Range(0f, 100f)]
    public float perlinStartX = 0f;
    [Range(0f, 100f)]
    public float perlinStartY = 0f;

    [Range(0f, 100f)]
    public float perlinEndX = 5f;
    [Range(0f, 100f)]
    public float perlinEndY = 5f;

    [Range(1, 6)]
    public int octaves = 2;

    [Range(1, 16)]
    public int persistence = 2;

    private TerrainData data;
    private float[,] heightField;

    // Use this for initialization
    void Start ()
    {
        data = GetComponent<Terrain>().terrainData;
        data.size = new Vector3((float)size, 10f, (float)size);
        data.heightmapResolution = size + 1;
        heightField = new float[size, size];
	}
	
	// Update is called once per frame
	void Update ()
    {
        GenerateHeights();
        data.SetHeights(0, 0, heightField);
	}

    void GenerateHeights()
    {
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                heightField[i, j] = CalculateHeight(i, j);
            }
        }
    }

    float CalculateHeight(int x, int y)
    {
        float noiseVal = 0f;
        float maxVal = 0f;

        float amp = 1;
        float frequency = 1f;

        for (int i = 0; i < octaves; ++i)
        {
            float perlinX = perlinStartX + ((float)x / (float)size) * (perlinEndX - perlinStartX) * frequency;
            float perlinY = perlinStartY + ((float)y / (float)size) * (perlinEndY - perlinStartY) * frequency;

            noiseVal += Mathf.PerlinNoise(perlinX, perlinY) * amp;

            maxVal += amp;
            amp *= persistence;
            frequency *= 2f;
        }

        return (noiseVal / maxVal);
    }
}

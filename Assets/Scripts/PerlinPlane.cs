﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class PerlinPlane : MonoBehaviour
{
    [Range(0,100)]
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
    public float perlinEndX = 100f;
    [Range(0f, 100f)]
    public float perlinEndY = 100f;

    [Range(1, 6)]
    public int octaves = 1;

    [Range(1,16)]
    public int persistence = 2;

    private Vector3[,] verts;

    // Use this for initialization
    void Start ()
    {
        verts = new Vector3[size,size];
        GenerateVerts();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshCreator mc = new MeshCreator();

        GenerateMesh(mc);

        meshFilter.mesh = mc.CreateMesh();
	}

    void GenerateVerts()
    {
        for(int i = 0; i < size; ++i)
        {
            for(int j = 0; j < size; ++j)
            {
                verts[i,j] = new Vector3(gridSize*i, 0f,gridSize*j);
            }
        }
    }

    void GenerateMesh(MeshCreator mc)
    {
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                verts[i, j].y = CalculateHeight(i, j);
            }
        }

        for (int i = 0; i < size - 1; ++i)
        {
            for (int j = 0; j < size - 1; ++j)
            {
                mc.BuildTriangle(verts[i, j], verts[i + 1, j], verts[i + 1, j + 1]);
                mc.BuildTriangle(verts[i, j], verts[i + 1, j + 1], verts[i, j + 1]);
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
            //Debug.Log(amp);
            frequency *= 2f;
        }

        return (noiseVal/maxVal)*bumpiness;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class PerlinPlane : MonoBehaviour
{
    public Texture2D guide;
    public GameObject enemyPrefab;
    public GameObject[] trees;
    public GameObject[] bushes;
    public GameObject[] grass;
    public GameObject[] rocks;
    public bool UpdateOnTick = false;
    [Range(0, 1f)]
    public float mixFraction = .8f;
    [Range(0,200)]
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

    private Vector3[][] verts;
    private MeshCollider MeshCol;

    // Use this for initialization
    void Start ()
    {
        verts = new Vector3[size][];
        for (int i = 0; i < size; ++i)
        {
            verts[i] = new Vector3[size];
        }
        MeshCol = GetComponent<MeshCollider>();
        GenerateVerts();

        if (!UpdateOnTick)
        {
            MeshFilter meshFilter = this.GetComponent<MeshFilter>();
            MeshCreator mc = new MeshCreator();
            mc.bumpiness = bumpiness;

            GenerateMesh(mc);
            GenerateEnvironment();

            meshFilter.mesh = mc.CreateMesh();
            MeshCol.sharedMesh = meshFilter.mesh;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (UpdateOnTick)
        {
            MeshFilter meshFilter = this.GetComponent<MeshFilter>();
            MeshCreator mc = new MeshCreator();
            mc.bumpiness = bumpiness;

            GenerateMesh(mc);

            meshFilter.mesh = mc.CreateMesh();
            MeshCol.sharedMesh = meshFilter.mesh;
        }
	}

    void GenerateVerts()
    {
        for(int i = 0; i < size; ++i)
        {
            for(int j = 0; j < size; ++j)
            {
                verts[i][j] = new Vector3(gridSize*i, 0f,gridSize*j);
            }
        }
    }

    void GenerateMesh(MeshCreator mc)
    {
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                verts[i][j].y = CalculateHeight(i, j);
            }
        }

        for (int i = 0; i < size - 1; ++i)
        {
            for (int j = 0; j < size - 1; ++j)
            {
                mc.BuildTriangle(verts[i][j], verts[i + 1][j], verts[i + 1][j + 1]);
                mc.BuildTriangle(verts[i][j], verts[i + 1][j + 1], verts[i][j + 1]);
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

        float xfrac = (float)x / (float)size;
        float yfrac = (float)y / (float)size;
        float greyScaleVal = guide.GetPixelBilinear(yfrac, xfrac).grayscale;


        //return (noiseVal/maxVal)*bumpiness;
        return (1 - (greyScaleVal * mixFraction) + noiseVal * (1 - mixFraction))*bumpiness;
    }

    void GenerateEnvironment()
    {
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                //verts[i][j].y = CalculateHeight(i, j);

                float xfrac = (float)i / (float)size;
                float yfrac = (float)j / (float)size;
                float greyScaleVal = guide.GetPixelBilinear(yfrac, xfrac).grayscale;

                float picker = Random.Range(0f, 100f) - 7f * greyScaleVal;
                if (picker < 8f)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.x += i * 6f;
                    newPosition.z -= j * 6f;
                    newPosition.y = -42f - 15f + (bumpiness - verts[i][j].y) * 6f;

                    float arrPicker = Random.Range(0f, 100f);

                    if(arrPicker < 40f) // grass
                    {
                        int index = Mathf.FloorToInt(Random.Range(0f, grass.Length));
                        GameObject obj = Instantiate(grass[index], newPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
                    }
                    else if (arrPicker < 60f) // rocks
                    {
                        int index = Mathf.FloorToInt(Random.Range(0f, rocks.Length));
                        GameObject obj = Instantiate(rocks[index], newPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
                    }
                    else if (arrPicker < 80f) // bushes
                    {
                        int index = Mathf.FloorToInt(Random.Range(0f, bushes.Length));
                        GameObject obj = Instantiate(bushes[index], newPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
                    }
                    else // trees
                    {
                        int index = Mathf.FloorToInt(Random.Range(0f, trees.Length));
                        GameObject obj = Instantiate(trees[index], newPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
                    }
                }
                else if (picker < 15f)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.x += i * 6f;
                    newPosition.z -= j * 6f;
                    newPosition.y = -42f - 15f + (bumpiness - verts[i][j].y) * 6f;

                    GameObject obj = Instantiate(enemyPrefab, newPosition, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                }
            }
        }
    }
}

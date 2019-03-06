using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator
{
    public float bumpiness = 10f;
    [Range(0, 1f)]
    public float color1Cutoff = .3f;
    [Range(0, 1f)]
    public float color2Cutoff = .7f;
    private List<Vector3> vertices = new List<Vector3>();

    private List<Vector3> normals = new List<Vector3>();
    private List<Vector2> uvs = new List<Vector2>();

    private List<int> triangleIndices = new List<int>();

    public MeshCreator()
    {

    }

    public void BuildTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2, Vector3 normal)
    {
        int v0Index = vertices.Count;
        int v1Index = vertices.Count + 1;
        int v2Index = vertices.Count + 2;

        // Put vertex data into vertices array 
        vertices.Add(vertex0);
        vertices.Add(vertex1);
        vertices.Add(vertex2);

        // Use the same normal for all vertices (i.e., a surface normal) 
        // Could change function signature to pass in 3 normals if needed  
        normals.Add(normal);
        normals.Add(normal);
        normals.Add(normal);

        float loc = 0f;

        float avgHeight = (vertex0.y + vertex1.y + vertex2.y) / (3f * (bumpiness + 2f));

        if (avgHeight < color1Cutoff)
            loc = .8f;
        else if (avgHeight < color2Cutoff)
            loc = .5f;
        else
            loc = 0f;

        /*/// Use standard uv coordinates  
        uvs.Add(new Vector2(.4f, .4f));
        uvs.Add(new Vector2(0, 0.3f));
        uvs.Add(new Vector2(0.3f, 0.3f));*/
        uvs.Add(new Vector2(loc, loc));
        uvs.Add(new Vector2(loc, loc + .1f));
        uvs.Add(new Vector2(loc + .1f, loc + .1f));

        // Add integer pointers to vertices into triangles list 
        triangleIndices.Add(v0Index);
        triangleIndices.Add(v1Index);
        triangleIndices.Add(v2Index); 
    }

    public void BuildTriangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
    {
        Vector3 normal = Vector3.Cross(vertex1 - vertex0, vertex2 - vertex0).normalized;
        BuildTriangle(vertex0, vertex1, vertex2, normal);
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = triangleIndices.ToArray();
        return mesh;
    }
}

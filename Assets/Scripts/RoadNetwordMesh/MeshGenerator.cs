using System.Collections.Generic;
using UnityEngine;

public abstract class MeshGenerator
{
    public Mesh mesh;

    protected readonly List<Vector3> vertices;
    protected readonly List<int> triangles;
    protected readonly List<Vector2> uvs;
    protected readonly List<Vector3> normals;

    protected MeshGenerator()
    {
        mesh = new Mesh();

        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();
        normals = new List<Vector3>();
    }
    
    protected void UpdateMesh()
    {
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uvs);
        mesh.SetNormals(normals);
    }

    protected void ClearMesh()
    {
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();
        normals.Clear();
    }

    protected MeshData GetData()
    {
        return new MeshData(vertices, triangles, uvs, normals);
    }
}
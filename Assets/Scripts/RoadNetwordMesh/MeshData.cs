using System.Collections.Generic;
using UnityEngine;

public class MeshData
{   
    public readonly List<Vector3> vertices;
    public readonly List<int> triangles;
    public readonly List<Vector2> uvs;
    public readonly List<Vector3> normals;

    public MeshData(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs, List<Vector3> normals)
    {
        this.vertices = vertices;
        this.triangles = triangles;
        this.uvs = uvs;
        this.normals = normals;
    }
}
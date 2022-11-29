using UnityEngine;

public class RoadMeshData
{
    public Road road;
    public LineSegment[] lines;
    public Mesh mesh;

    public RoadMeshData(Road road)
    {
        this.road = road;
        lines = road.GetLines();
        mesh = new Mesh();
    }

    public void SetLines()
    {
        lines = road.GetLines();
    }

    public void SetMesh(MeshData meshData)
    {
        mesh.Clear();
        mesh.SetVertices(meshData.vertices);
        mesh.SetTriangles(meshData.triangles, 0);
        mesh.SetUVs(0, meshData.uvs);
        mesh.SetNormals(meshData.normals);
    }
}
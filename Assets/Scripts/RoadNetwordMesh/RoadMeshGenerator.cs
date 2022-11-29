using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class RoadMeshGenerator : MeshGenerator
{
    protected List<RoadMeshData> roadMeshData;
    private Material material;

    protected RoadMeshGenerator(Material material) : base()
    {
        roadMeshData = new List<RoadMeshData>();
        this.material = material;
    }

    public void DrawRoadMesh()
    {
        foreach (var meshData in roadMeshData) 
            Graphics.DrawMesh(meshData.mesh, Matrix4x4.identity, material, 0);
    }

    public void AddRoad(Road road)
    {
        var meshData = new RoadMeshData(road);
        roadMeshData.Add(meshData);
        meshData.SetMesh(GenerateRoadMesh(meshData));
    }
    
    public void RemoveRoad(Road road)
    {
        var meshData = roadMeshData.FirstOrDefault(m => m.road == road);
        roadMeshData.Remove(meshData);
    }

    public void EditRoad(Road road)
    {
        var meshData = roadMeshData.FirstOrDefault(m => m.road == road);
        if (meshData != null && roadMeshData.Contains(meshData))
        {
            meshData.SetLines();
            meshData.SetMesh(GenerateRoadMesh(meshData));
        }
    }

    protected abstract MeshData GenerateRoadMesh(RoadMeshData line);
}
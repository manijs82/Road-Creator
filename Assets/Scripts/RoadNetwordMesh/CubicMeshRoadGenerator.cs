using UnityEngine;

public class CubicMeshRoadGenerator : RoadMeshGenerator
{
    private float width = RoadNetwork.GridSize;
    private float roadPortion = .50f;
    private float sidePortion = .50f;
    private float roadY = .2f;
    private float sideWalkY = .4f;
    private int uvSegments = 3;
    private int cubeCount;

    public CubicMeshRoadGenerator(Material material) : base(material)
    {
    }

    protected override MeshData GenerateRoadMesh(RoadMeshData meshData)
    {
        cubeCount = 0;
        ClearMesh();
        
        foreach (var line in meshData.lines)
        {
            if(line.Magnitude > 0.1f)
                GenerateCubeMesh(line);
        }

        return GetData();
    }

    private void GenerateCubeMesh(LineSegment line)
    {
        var roadHalfWidth = roadPortion * width / 2;
        var sideWalkHalfWidth = sidePortion * width / 4;
        
        var rightOffset = Vector3.Cross(line.Direction, Vector3.up) * (roadHalfWidth + sideWalkHalfWidth);
        var leftOffset = -rightOffset;

        CreateCube(line.start, line.end,
            roadHalfWidth, roadY, 1, line.isHorizontal);

        CreateCube(line.start + rightOffset, line.end + rightOffset,
            sideWalkHalfWidth, sideWalkY, -0.1f, line.isHorizontal);

        CreateCube(line.start + leftOffset, line.end + leftOffset,
            sideWalkHalfWidth, sideWalkY, 2, line.isHorizontal);
    }

    private void CreateCube(Vector3 bottom, Vector3 top, float xOffset, float yOffset, float uvIndex, bool isHorizontal)
    {
        AddVerts(bottom, top, xOffset, yOffset, isHorizontal);
        AddTris();
        AddUvs(uvIndex, (top - bottom).magnitude / width, isHorizontal);
        AddNormals();
        
        cubeCount++;
    }

    private void AddVerts(Vector3 bottom, Vector3 top, float xOffset, float yOffset, bool isHorizontal)
    {
        vertices.AddRange(CubeMesh.GerVerts(bottom, top, xOffset, yOffset, isHorizontal));
    }

    private void AddTris()
    {
        triangles.AddRange(CubeMesh.GetTris(cubeCount));
    }

    private void AddUvs(float segment, float size, bool isHorizontal)
    {
        uvs.AddRange(CubeMesh.GetUvs(segment, uvSegments, size, isHorizontal));
    }
    
    private void AddNormals()
    {
        normals.AddRange(new []
        {
            Vector3.up,
            Vector3.up, 
            Vector3.up, 
            Vector3.up, 
            Vector3.up, 
            Vector3.up, 
            Vector3.up, 
            Vector3.up 
        });
    }
}
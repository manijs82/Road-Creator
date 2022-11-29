using System.Collections.Generic;
using UnityEngine;

public class QuadRoadGenerator
{
    private readonly List<Vector3> vertices;
    private readonly List<int> triangles;
    private readonly List<Vector2> uvs;
    private int roadCount;

    private void GenerateRoadMesh(LineSegment line)
    {
        CreateQuad(line);
    }

    private void CreateQuad(LineSegment line)
    {
        var bottom = new Vector3(line.start.x, 0, line.start.y);
        var top = new Vector3(line.end.x, 0, line.end.y);
        
        vertices.AddRange(VectorExtensions.GetSquare(bottom, top, 1.5f, 
            line.isHorizontal ? Direction.East : Direction.North));

        var trisOffset = roadCount * 4;
        int[] tris = 
        {
            // lower left triangle
            0 + trisOffset, 2 + trisOffset, 1 + trisOffset,
            // upper right triangle
            2 + trisOffset, 3 + trisOffset, 1 + trisOffset
        };
        triangles.AddRange(tris);

        if (!line.isHorizontal)
        {
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(1, 1));
        }
        else
        {
            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(1, 1));
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(0, 1));
        }

        roadCount++;
    }
}
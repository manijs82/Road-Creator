using UnityEngine;

public static class CubeMesh
{
    private static readonly int[] TrisArray =
    {
        0, 4, 1, //front
        4, 5, 1,

        4, 6, 5, //up
        6, 7, 5,

        2, 6, 0, //right
        6, 4, 0,

        1, 5, 3, //left
        5, 7, 3,

        3, 7, 2, //behind
        7, 6, 2
    };

    public static Vector3[] GerVerts(Vector3 bottom, Vector3 top, float xOffset, float yOffset, bool isHorizontal)
    {
        var result = new Vector3[8];
        
        var bottomArray = VectorExtensions.GetSquare(bottom, top, xOffset, isHorizontal ? Direction.East : Direction.North);
        var topArray = VectorExtensions.GetSquare(bottom + Vector3.up * yOffset, top + Vector3.up * yOffset, xOffset,
            isHorizontal ? Direction.East : Direction.North);

        for (int i = 0; i <= 3; i++) 
            result[i] = bottomArray[i];

        for (int i = 0; i <= 3; i++) 
            result[i + 4] = topArray[i];

        return result;
    }
    
    public static int[] GetTris(int offset)
    {
        var result = new int[30];
        
        var trisOffset = offset * 8;
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = TrisArray[i] + trisOffset;
        }
        
        return result;
    }

    public static Vector2[] GetUvs(float segment, float uvSegments, float size, bool isHorizontal)
    {
        var p1 = Mathf.Clamp01(segment / uvSegments);
        var p2 = Mathf.Clamp01((segment + 1f) / uvSegments);

        if (isHorizontal)
        {
            return new[]
            {
                new Vector2(p2, 0),
                new Vector2(p2, size),
                new Vector2(p1, 0),
                new Vector2(p1, size),
            
                new Vector2(p2, 0),
                new Vector2(p2, size),
                new Vector2(p1, 0),
                new Vector2(p1, size)
            };   
        }
        else
        {
            return new[]
            {
                new Vector2(p1, 0),
                new Vector2(p2, 0),
                new Vector2(p1, size),
                new Vector2(p2, size),
            
                new Vector2(p1, 0),
                new Vector2(p2, 0),
                new Vector2(p1, size),
                new Vector2(p2, size)
            };
        }
    }
}
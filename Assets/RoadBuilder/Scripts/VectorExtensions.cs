using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 Mul(this Vector3 v, Vector3 v2)
    {
        return new Vector3(v.x * v2.x, v.y * v2.y, v.z * v2.z);
    }
    
    public static Vector3[] GetSquare(Vector3 bottom, Vector3 top, float offset, Direction direction)
    {
        Vector3[] result = new Vector3[4];
        
        if (direction == Direction.North)
        {
            result[0] = bottom.GetLeft(offset);
            result[1] = bottom.GetRight(offset);
            result[2] = top.GetLeft(offset);
            result[3] = top.GetRight(offset);
        }

        if (direction == Direction.East)
        {
            result[0] = bottom.GetBottom(offset);
            result[1] = top.GetBottom(offset);
            result[2] = bottom.GetTop(offset);
            result[3] = top.GetTop(offset);
        }

        return result;
    }

    public static Vector3 GetBottomLeft(this Vector3 v, float offset) =>
        new Vector3(v.x - offset, v.y, v.z - offset);
    
    public static Vector3 GetBottomRight(this Vector3 v, float offset) =>
        new Vector3(v.x + offset, v.y, v.z - offset);
    
    public static Vector3 GetTopLeft(this Vector3 v, float offset) =>
        new Vector3(v.x - offset, v.y, v.z + offset);
    
    public static Vector3 GetTopRight(this Vector3 v, float offset) =>
        new Vector3(v.x + offset, v.y, v.z + offset);
    
    public static Vector3 GetLeft(this Vector3 v, float offset) =>
        new Vector3(v.x - offset, v.y, v.z);
    
    public static Vector3 GetRight(this Vector3 v, float offset) =>
        new Vector3(v.x + offset, v.y, v.z);
    
    public static Vector3 GetTop(this Vector3 v, float offset) =>
        new Vector3(v.x, v.y, v.z + offset);
    
    public static Vector3 GetBottom(this Vector3 v, float offset) =>
        new Vector3(v.x, v.y, v.z - offset);
}
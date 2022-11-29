using UnityEngine;

public static class DirectionUtils
{
    
    public static bool IsHorizontal(this Direction direction)
    {
        return direction is Direction.East or Direction.West;
    }
    
    public static bool IsVertical(this Direction direction)
    {
        return direction is Direction.North or Direction.South;
    }
    
    public static Vector2 GetVector2(this Direction direction)
    {
        return direction switch
        {
            Direction.North => Vector2.up,
            Direction.South => Vector2.down,
            Direction.East => Vector2.right,
            Direction.West => Vector2.left,
            _ => Vector2.zero
        };
    }
    
    public static Vector2Int GetVector2Int(this Direction direction)
    {
        return direction switch
        {
            Direction.North => Vector2Int.up,
            Direction.South => Vector2Int.down,
            Direction.East => Vector2Int.right,
            Direction.West => Vector2Int.left,
            _ => Vector2Int.zero
        };
    }
    
    public static Vector3 GetVector3(this Direction direction)
    {
        return direction switch
        {
            Direction.North => Vector3.forward,
            Direction.South => Vector3.back,
            Direction.East => Vector3.right,
            Direction.West => Vector3.left,
            _ => Vector3.zero
        };
    }
        
    public static Quaternion GetRotation(this Direction direction)
    {
        return direction switch
        {
            Direction.North => Quaternion.Euler(0, 0, 0),
            Direction.South => Quaternion.Euler(0, 180, 0),
            Direction.East => Quaternion.Euler(0, 90, 0),
            Direction.West => Quaternion.Euler(0, 270, 0),
            _ => Quaternion.identity
        };
    }
        
    public static int GetAngle(this Direction direction)
    {
        return direction switch
        {
            Direction.North => 0,
            Direction.South => 180,
            Direction.East => 90,
            Direction.West => 270,
            _ => 0
        };
    }
}
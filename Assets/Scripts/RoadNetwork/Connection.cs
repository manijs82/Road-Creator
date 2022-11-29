using System;
using UnityEngine;

public class Connection
{
    public event Action<Direction> OnChangeDirection;

    public bool selectedInEditor;
    public Road road1;
    
    private Vector2 point;

    public Vector2 Point => point;
    public Vector3 PointXZ => new (point.x, 0, point.y);
    public ConnectionType type;
    public Direction direction;
    
    public Connection()
    {
        point = Vector2Int.zero;
        direction = Direction.North;
    }

    public Connection(Vector2 point, Road road1, Direction direction = Direction.North)
    {
        this.point = point;
        this.direction = direction;
        this.road1 = road1;
        type = ConnectionType.Road;
    }

    public void SetDirection(Direction direction)
    {
        this.direction = direction;
        OnChangeDirection?.Invoke(direction);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadNetwork
{
    public const int GridSize = 3;
    
    public event Action<Connection> OnAddConnection;
    public event Action<Connection> OnRemoveConnection;

    private readonly List<Road> roads;
    private readonly Dictionary<Vector2Int, Connection> connections;

    public List<Road> Roads => roads;

    public RoadNetwork()
    {
        roads = new List<Road>();
        connections = new Dictionary<Vector2Int, Connection>();
    }

    public void AddRoad(Road road)
    {
        roads.Add(road);
    }

    public void RemoveRoad(Road road)
    {
        /* if (!Roads.Contains(road)) return;
        Roads.Remove(road);
        RemoveConnections(road);
        OnRemoveRoad?.Invoke(road); */
    }

    public void AddConnection(Connection[] cons)
    {
        var addedCons = new List<Connection>();
        foreach (var con in cons)
        {
            var pos = new Vector2Int(Mathf.RoundToInt(con.Point.x), Mathf.RoundToInt(con.Point.y));
            if(connections.TryAdd(pos, con))
                addedCons.Add(con);
        }

        foreach (var con in addedCons)
        {
            var pos = new Vector2Int(Mathf.RoundToInt(con.Point.x), Mathf.RoundToInt(con.Point.y));
            if(connections.TryGetValue(new Vector2Int(pos.x, pos.y + GridSize), out var c1)) SetType(c1);
            if(connections.TryGetValue(new Vector2Int(pos.x, pos.y - GridSize), out var c2)) SetType(c2);
            if(connections.TryGetValue(new Vector2Int(pos.x + GridSize, pos.y), out var c3)) SetType(c3);
            if(connections.TryGetValue(new Vector2Int(pos.x - GridSize, pos.y), out var c4)) SetType(c4);
            SetType(con);
            OnAddConnection?.Invoke(con);
        }
    }

    private void SetType(Connection connection)
    {
        var pos = new Vector2Int(Mathf.RoundToInt(connection.Point.x), Mathf.RoundToInt(connection.Point.y));
        int trueCount = 0;
        bool up = connections.ContainsKey(new Vector2Int(pos.x, pos.y + GridSize));
        bool down = connections.ContainsKey(new Vector2Int(pos.x, pos.y - GridSize));
        bool right = connections.ContainsKey(new Vector2Int(pos.x + GridSize, pos.y));
        bool left = connections.ContainsKey(new Vector2Int(pos.x - GridSize, pos.y));
        trueCount += up ? 1 : 0;
        trueCount += down ? 1 : 0;
        trueCount += right ? 1 : 0;
        trueCount += left ? 1 : 0;
        

        if (trueCount == 4)
        {
            connection.type = ConnectionType.Intersection;
        }

        if (trueCount == 1)
        {
            connection.type = ConnectionType.End;
            if (down)
                connection.direction = Direction.South;
            if (left)
                connection.direction = Direction.West;
            if (up)
                connection.direction = Direction.North;
            if (right)
                connection.direction = Direction.East;
        }
    }

    public void RemoveConnection(Vector2Int pos)
    {
        if(connections.ContainsKey(pos))
        {
            connections.Remove(pos, out Connection connection);
            if(connections.TryGetValue(new Vector2Int(pos.x, pos.y + GridSize), out var c1)) SetType(c1);
            if(connections.TryGetValue(new Vector2Int(pos.x, pos.y - GridSize), out var c2)) SetType(c2);
            if(connections.TryGetValue(new Vector2Int(pos.x + GridSize, pos.y), out var c3)) SetType(c3);
            if(connections.TryGetValue(new Vector2Int(pos.x - GridSize, pos.y), out var c4)) SetType(c4);
            
            OnRemoveConnection?.Invoke(connection);
        }
    }

    public Connection[] GetIntersections()
    {
        return connections.Values.ToArray();
    }

    public void DrawAllGizmos()
    {
        foreach (var connection in connections)
            DrawConnection(connection.Value);
    }

    private void DrawConnection(Connection connection)
    {
        Gizmos.color = connection.selectedInEditor ? Color.yellow : Color.Lerp(Color.yellow, Color.cyan, .5f);
        var r = connection.selectedInEditor ? 1.5f : 1f;
        Gizmos.DrawCube(connection.PointXZ, Vector3.one * r);
    }
}
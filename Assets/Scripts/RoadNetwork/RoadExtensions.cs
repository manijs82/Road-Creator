using System.Collections.Generic;
using UnityEngine;

public static class RoadExtensions
{
   public static Vector3[] GetPoints(this Road road, int interval)
    {
        var result = new Vector3[road.roadLine.Magnitude / interval + 1];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = road.roadLine.GetPoint(i * interval);
        }

        return result;
    }

    public static LineSegment[] GetLines(this Road road)
    {
        /* var inters = SortByDistance(road.Intersections, road.Start.Point);
        var result = new LineSegment[inters.Count + 1];

        var offset2d = road.Start.Direction.GetVector2Int() * RoadNetwork.GridSize;
        var offset = new Vector3(offset2d.x, 0, offset2d.y)/2;

        if (result.Length == 1)
        {
            result[0] = new LineSegment(road.Start.PointXZ + offset, road.End.PointXZ - offset, road.IsHorizontal);
            return result;
        }

        result[0] = new LineSegment(road.Start.PointXZ + offset, inters[0].PointXZ - offset, road.IsHorizontal);
        result[^1] = new LineSegment(inters[^1].PointXZ + offset, road.End.PointXZ - offset, road.IsHorizontal);

        for (int i = 1; i < inters.Count; i++)
        {
            result[i] = 
                new LineSegment(inters[i - 1].PointXZ + offset, inters[i].PointXZ - offset, road.IsHorizontal);
        } */

        return null;
    }
    
    private static List<Connection> SortByDistance(List<Connection> lst, Vector2Int start)
    {
        var tempList = new List<Connection>(lst);
        var output = new List<Connection>();

        for (int i = 0; i < lst.Count; i++)
        {
            Connection smallestDistanceConnection = new Connection();
            float smallestDistance = float.MaxValue;
            
            foreach (var connection in tempList)
            {
                float distance = (connection.Point - start).magnitude;
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    smallestDistanceConnection = connection;
                }
            }

            output.Add(smallestDistanceConnection);
            tempList.Remove(smallestDistanceConnection);
            
        }

        return output;
    }     
}
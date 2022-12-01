using UnityEngine;

public class RoadAdjuster : RoadNetworkEditor
{

    public RoadAdjuster(RoadNetwork network) : base(network)
    {
    }

    public override void EditNetwork(Line line)
    {
        AdjustRoad(line);
    }

    private void AdjustRoad(Line line)
    {
        /* var connection = GetRoadConnection(line.start, out Road road);
        if (connection == null) return;

        var newPoint = GetStraightPoint(line.end, connection);
        var oldPoint = connection.Point;
        
        connection.ChangePoint(newPoint);
        if(!CanAdjustRoad(road)) 
            connection.ChangePoint(oldPoint);
        RoadEdited(road); */
    }
    
    private Vector2Int GetStraightPoint(Vector2Int point, Connection connection)
    {
        var result = point;
        if (connection.direction == Direction.West ||
            connection.direction == Direction.East) 
            result.y = (int) connection.Point.y;
        if (connection.direction == Direction.North ||
            connection.direction == Direction.South)
            result.x = (int) connection.Point.x;

        return result;
    }
}
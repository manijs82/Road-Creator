using System.Collections.Generic;

public class RoadCreator : RoadNetworkEditor
{
    public RoadCreator(RoadNetwork network) : base(network)
    {
    }

    public override void EditNetwork(Line line)
    {
        AddRoad(line);
    }

    private void AddRoad(Line line)
    {
        line.Straighten();
        line.Sort();

        if (!CanPlaceRoad(line)) return;

        var points = line.GetPoints(RoadNetwork.GridSize);
        var road = new Road(line);
        
        var connections = new List<Connection>();
        foreach (var point in points)
            connections.Add(new Connection(point, road, road.IsHorizontal ? Direction.East : Direction.North));
        AddConnections(connections.ToArray());
    }

    private bool CanPlaceRoad(Line line)
    {
        return line.Magnitude > RoadNetwork.GridSize && !line.IsDiagonalLine;
    }
}
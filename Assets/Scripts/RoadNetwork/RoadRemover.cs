﻿using System.Linq;
using UnityEngine;

public class RoadRemover : RoadNetworkEditor
{
    public RoadRemover(RoadNetwork network) : base(network)
    {
    }

    public override void EditNetwork(Vector2Int point)
    {
        RemoveRoad(point);
    }

    private void RemoveRoad(Vector2Int point)
    {
        RemoveConnection(point);
    }

    private Road GetRoad(Vector2Int point) => 
        Roads.FirstOrDefault(r => r.HasPoint(point));
}
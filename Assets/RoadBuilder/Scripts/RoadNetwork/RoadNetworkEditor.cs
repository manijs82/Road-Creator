using System;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetworkEditor
{
    
    protected readonly RoadNetwork network;

    protected RoadNetworkEditor(RoadNetwork network)
    {
        this.network = network;
    }

    public virtual void EditNetwork(Line line) { }
    public virtual void EditNetwork(Vector2Int point) { }

    protected void AddConnections(Connection[] connections)
    {
        network.AddConnection(connections);
    }
    
    protected void RemoveConnection(Vector2Int pos)
    {
        network.RemoveConnection(pos);
    }
}
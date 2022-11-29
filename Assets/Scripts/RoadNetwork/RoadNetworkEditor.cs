using System;
using System.Collections.Generic;
using UnityEngine;

public class RoadNetworkEditor
{
    public static event Action<Road> OnEditRoad;
    
    protected readonly RoadNetwork network;
    protected List<Road> Roads => network.Roads;

    protected RoadNetworkEditor(RoadNetwork network)
    {
        this.network = network;
    }

    public virtual void EditNetwork(Line line) { }
    public virtual void EditNetwork(Vector2Int point) { }

    protected void RoadEdited(Road road) => 
        OnEditRoad?.Invoke(road);

    protected void AddConnections(Connection[] connections)
    {
        network.AddConnection(connections);
    }
    
    protected void RemoveConnection(Vector2Int pos)
    {
        network.RemoveConnection(pos);
    }
}
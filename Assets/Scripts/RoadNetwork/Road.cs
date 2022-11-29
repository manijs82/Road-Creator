using UnityEngine;

public class Road
{
    public Line roadLine;

    private readonly RoadType roadType;
    public bool IsHorizontal => roadType == RoadType.Horizontal;

    public Road(Line roadLine)
    {
        this.roadLine = roadLine;

        roadType = roadLine.IsHorizontalLine ? RoadType.Horizontal : RoadType.Vertical;
    }
    
    public bool HasPoint(Vector2Int point) =>
        roadLine.OnLine(point);
    
    public bool HasPointInside(Vector2Int point) =>
        roadLine.InsideLine(point);
}

public enum RoadType
{
    Horizontal,
    Vertical
}
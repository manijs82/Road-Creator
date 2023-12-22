using UnityEngine;

public struct Line
{
    public Vector2Int start;
    public Vector2Int end;

    #region Properties

    public Vector3 StartXZ => new(start.x, 0, start.y);
    public Vector3 EndXZ => new(end.x, 0, end.y);
    public Vector2 Displacement => end - start;
    public Vector3 DisplacementXZ => EndXZ - StartXZ;
    public int Magnitude => (int)Displacement.magnitude;
    public Vector2 Direction => Displacement.normalized;
    public Vector3 DirectionXZ => DisplacementXZ.normalized;
    public float Inversion => Vector2.Dot(Direction, new Vector2(Mathf.Abs(Direction.x), Mathf.Abs(Direction.y)));
    public bool Inverted => Inversion < 0;
    public int XDifference => Mathf.Abs(end.x - start.x);
    public int YDifference => Mathf.Abs(end.y - start.y);
    public float SignedXDifference => end.x - start.x;
    public float SignedYDifference => end.y - start.y;
    public bool IsHorizontalLine => start.y == end.y;
    public bool IsDiagonalLine => XDifference == YDifference;

    #endregion

    public Line(Vector2Int start, Vector2Int end)
    {
        this.start = start;
        this.end = end;
    }

    public Vector2 GetPoint(int distance)
    {
        var t = Mathf.Clamp01((float)distance / Magnitude);
        return GetPoint(t);
    }
    
    public Vector2 GetPoint(float t)
    {
        var x = start.x + t * (SignedXDifference);
        var y = start.y + t * (SignedYDifference);
        return new Vector2(x, y);
    }

    public bool OnLine(Vector2Int p)
    {
        if (IsHorizontalLine)
            return (start.x <= p.x && p.x <= end.x) && p.y == start.y;
        else
            return (start.y <= p.y && p.y <= end.y) && p.x == start.x;
    }
    
    public bool InsideLine(Vector2Int p)
    {
        if (IsHorizontalLine)
            return (start.x < p.x && p.x < end.x) && p.y == start.y;
        else
            return (start.y < p.y && p.y < end.y) && p.x == start.x;
    }

    public override string ToString()
    {
        return start + " - " + end;
    }
}
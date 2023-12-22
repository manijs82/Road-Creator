using UnityEngine;

public static class LineExtensions
{
    const float ParallelDeterminantThreshold = 0.00001f;
    
    public static void Sort(ref this Line line)
    {
        int minX = Mathf.Min(line.start.x, line.end.x);
        int maxX = Mathf.Max(line.start.x, line.end.x);
        int minY = Mathf.Min(line.start.y, line.end.y);
        int maxY = Mathf.Max(line.start.y, line.end.y);

        line.start = new Vector2Int(minX, minY);
        line.end = new Vector2Int(maxX, maxY);
    }

    public static void Straighten(ref this Line line)
    {
        if (line.XDifference < line.YDifference)
            line.end.x = line.start.x;

        if (line.YDifference < line.XDifference)
            line.end.y = line.start.y;
    }
    
    public static Vector2[] GetPoints(this Line line, int gridSize)
    {
        var o = new Vector2[line.Magnitude / gridSize + 1];
        for (int i = 0; i < o.Length; i++)
        {
            o[i] = line.GetPoint(i*gridSize);
        }
        return o;
    }
    
    public static Line Move(this Line line, Vector2Int direction)
    {
        line.start += direction;
        line.end += direction;

        return line;
    }
    
    public static bool LineLineIntersection(Line line1, Line line2, out Vector2 intersection)
    {
        intersection = Vector2.zero;

        var d = Determinant(line1.Displacement, line2.Displacement);

        if (Mathf.Abs(d) < ParallelDeterminantThreshold)
            return false;

        Vector2 line1To2 = line2.start - line1.start;

        var tA = Determinant(line1To2, line2.Displacement) / d;
        var tB = Determinant(line1To2, line1.Displacement) / d;

        if (tA <= 0.0f || tA >= 1.0f || tB <= 0.0f || tB >= 1.0f)
            return false;

        intersection = line1.GetPoint(tA);

        return true;
    }
    
    public static bool AreParallelLines(Line line1, Line line2)
    {
        var d = Determinant(line1.Displacement, line2.Displacement);

        if (Mathf.Abs(d) < ParallelDeterminantThreshold)
        {
            Vector2 line1To2 = line2.start - line1.start;

            var tA = Determinant(line1To2, line2.Displacement) / d;
            var tB = Determinant(line1To2, line1.Displacement) / d;
            
            if (Mathf.Abs(tA) > ParallelDeterminantThreshold && 
                Mathf.Abs(tB) > ParallelDeterminantThreshold)
                return true;
        }

        return false;
    }
    
    public static bool Overlap(Line line1 ,Line line2)
    {
        var d = Determinant(line1.Displacement, line2.Displacement);

        return Mathf.Abs(d) < ParallelDeterminantThreshold && 
               (line1.InsideLine(line2.start) || line1.InsideLine(line2.end) ||
                line2.InsideLine(line1.start) || line2.InsideLine(line1.end));
    }
    
    public static bool Collinear(Line line1 ,Line line2)
    {
        if (line1.IsHorizontalLine && line2.IsHorizontalLine)
            return line1.start.y == line2.start.y;
        if (!line1.IsHorizontalLine && !line2.IsHorizontalLine)
            return line1.start.x == line2.start.x;

        return false;
    }

    public static float Determinant(Vector2 a, Vector2 b) => 
        a.x * b.y - a.y * b.x;
}
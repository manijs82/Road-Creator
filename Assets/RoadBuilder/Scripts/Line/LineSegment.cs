using UnityEngine;

public class LineSegment
{
    public Vector3 start;
    public Vector3 end;
    public bool isHorizontal;
    
    public Vector3 Displacement => end - start;
    public Vector3 Direction => Displacement.normalized;
    public float Magnitude => Displacement.magnitude;

    public LineSegment(Vector3 start, Vector3 end, bool isHorizontal)
    {
        this.start = start;
        this.end = end;
        this.isHorizontal = isHorizontal;
    }

    public override string ToString()
    {
        return start + " - " + end;
    }
}
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Grid
{
    private int width;
    private int height;
    private int cellSize;
    private Vector3 origin;

    public int Height => height;

    public int Width => width;

    public int CellSize => cellSize;

    public Grid(int width, int height, int cellSize, Vector3 origin = default)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;
    }

#if UNITY_EDITOR
    public void DrawGrid()
    {
        Handles.zTest = CompareFunction.LessEqual;
            
        int halfWidth = width / 2;
        int halfHeight = height / 2;
        float halfCellSize = cellSize / 2f;
        for (int x = -halfWidth; x <= halfWidth; x++)
        {
            Vector3 lineX = GetWorldPosition(x, -halfHeight);
            Vector3 lineY = GetWorldPosition(x, halfHeight);
            Handles.DrawAAPolyLine(lineX, lineY);
        }
        for (int z = -halfHeight; z <= halfHeight; z++)
        {
            Vector3 lineX = GetWorldPosition(-halfWidth, z);
            Vector3 lineY = GetWorldPosition(halfWidth, z);
            Handles.DrawAAPolyLine(lineX, lineY);
        }
    }
#endif

    public Vector3 GetWorldPosition(float x, float z) => new Vector3(x, 0, z) * cellSize + origin;
        
    public void GetGridXZ(Vector3 pos, out int x, out int z)
    {
        x = Mathf.FloorToInt((pos - origin).x / cellSize);
        z = Mathf.FloorToInt((pos - origin).z / cellSize);
    }
        
    public Vector3 GetGridPosition(Vector3 pos)
    {
        float x = Mathf.Round(pos.x / cellSize) * cellSize;
        float z = Mathf.Round(pos.z / cellSize) * cellSize;

        return new Vector3(x + origin.x, origin.y, z + origin.z);
    }
        
    public Vector3Int GetGridPositionInt(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x / cellSize) * cellSize;
        int z = Mathf.RoundToInt(pos.z / cellSize) * cellSize;

        return new Vector3Int(x + (int) origin.x, (int) origin.y, z + (int) origin.z);
    }
}
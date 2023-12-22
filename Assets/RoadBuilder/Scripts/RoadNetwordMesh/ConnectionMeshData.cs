using UnityEngine;

public class ConnectionMeshData
{
    public Matrix4x4 matrix;
    public Connection connection;

    public ConnectionMeshData(Connection connection)
    {
        this.connection = connection;

        CalculateMatrix();
        connection.OnChangeDirection += _ => CalculateMatrix();
    }

    private void CalculateMatrix()
    {
        var p = connection.PointXZ;
        var r = connection.direction.GetRotation() * Quaternion.Euler(0, 180, 0);

        matrix = Matrix4x4.TRS(p, r, new Vector3(RoadNetwork.GridSize, 1, RoadNetwork.GridSize));
    }
}
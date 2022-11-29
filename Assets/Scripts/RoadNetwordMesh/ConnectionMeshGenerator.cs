using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ConnectionMeshGenerator
{
    private ConnectionMeshes meshesData;
    private List<ConnectionMeshData> connectionMeshData;
    private List<Mesh> meshes; 

    public ConnectionMeshGenerator(ConnectionMeshes meshesData)
    {
        this.meshesData = meshesData;
        meshes = new List<Mesh>();
        foreach (var meshData in meshesData.connectionMeshes) meshes.Add(meshData.mesh);
        connectionMeshData = new List<ConnectionMeshData>();

        CorrectNormals();
    }
    
    private void CorrectNormals()
    {
        foreach (var mesh in meshesData.connectionMeshes)
        {
            var normals = new Vector3[mesh.mesh.normals.Length];
            for (var i = 0; i < normals.Length; i++)
            {
                normals[i] = Vector3.up;
            }
            mesh.mesh.SetNormals(normals);
        }
    }

    public void AddConnection(Connection connection)
    {
        connectionMeshData.Add(new ConnectionMeshData(connection));
    }

    public void RemoveConnection(Connection connection)
    {
        var con = connectionMeshData.FirstOrDefault(c => c.connection == connection);
        if (con != null && connectionMeshData.Contains(con))
            connectionMeshData.Remove(con);
    }

    public void DrawConnectionMesh()
    {
        foreach (var meshData in connectionMeshData)
        {
            Graphics.DrawMesh(GetMesh(meshData.connection.type), 
                meshData.matrix, meshesData.connectionsMaterial, 0);
        }
    }

    private Mesh GetMesh(ConnectionType type)
    {
        return meshes[(int) type];
    }

    private void DrawMeshesInstanced()
    {
        Graphics.DrawMeshInstanced(meshesData.connectionMeshes[0].mesh,
            0, meshesData.connectionsMaterial, GetMatrices(), null,
            ShadowCastingMode.On, true);
    }

    private List<Matrix4x4> GetMatrices()
    {
        return connectionMeshData.Select(c => c.matrix).ToList();
    }
}
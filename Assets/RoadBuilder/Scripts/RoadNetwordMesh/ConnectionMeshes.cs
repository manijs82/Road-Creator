using UnityEngine;

[CreateAssetMenu(fileName = "ConnectionMeshes", menuName = "ConnectionMeshes", order = 0)]
public class ConnectionMeshes : ScriptableObject
{
    public Material connectionsMaterial;
    public ConnectionMesh[] connectionMeshes;
}

[System.Serializable]
public class ConnectionMesh
{
    public ConnectionType connectionType;
    public Mesh mesh;
}
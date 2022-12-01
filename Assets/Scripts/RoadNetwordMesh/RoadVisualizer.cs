using UnityEngine;

public class RoadVisualizer : MonoBehaviour
{
    [SerializeField] private RoadNetworkCreator creator;
    [SerializeField] private ConnectionMeshes connectionMeshes;
    
    private ConnectionMeshGenerator connectionMeshGenerator;
    
    private void Start()
    {
        connectionMeshGenerator = new ConnectionMeshGenerator(connectionMeshes);

        creator.roadNetwork.OnAddConnection += OnAddConnection;
        creator.roadNetwork.OnRemoveConnection += OnRemoveConnection;
    }

    private void Update()
    {
        //roadMeshGenerator.DrawRoadMesh();
        connectionMeshGenerator.DrawConnectionMesh();
    }
    
    private void OnAddConnection(Connection connection)
    {
        connectionMeshGenerator.AddConnection(connection);
    }
    
    private void OnRemoveConnection(Connection connection)
    {
        connectionMeshGenerator.RemoveConnection(connection);
    }
}
using UnityEngine;

public class RoadVisualizer : MonoBehaviour
{
    [SerializeField] private RoadNetworkCreator creator;
    [SerializeField] private ConnectionMeshes connectionMeshes;
    
    private RoadMeshGenerator roadMeshGenerator;
    private ConnectionMeshGenerator connectionMeshGenerator;
    
    private void Start()
    {
        roadMeshGenerator = new CubicMeshRoadGenerator(connectionMeshes.connectionsMaterial);
        connectionMeshGenerator = new ConnectionMeshGenerator(connectionMeshes);

        creator.roadNetwork.OnAddConnection += OnAddConnection;
        creator.roadNetwork.OnRemoveConnection += OnRemoveConnection;
        RoadNetworkEditor.OnEditRoad += OnEditRoad;
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
    
    private void OnEditRoad(Road road)
    {
        roadMeshGenerator.EditRoad(road);
    }
}
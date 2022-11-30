using UnityEngine;

public class RoadNetworkCreator : MonoBehaviour
{
    [SerializeField] private TileMap tileMap;
    
    public RoadNetwork roadNetwork;

    private RoadNetworkEditor roadCreator;
    private RoadNetworkEditor roadAdjuster;
    private RoadNetworkEditor roadRemover;
    
    private Grid grid;
    private Plane plane;
    private Vector2Int firstClickPoint;
    private Vector2Int secondClickPoint;

    private void Awake()
    {
        roadNetwork = new RoadNetwork(tileMap);
        grid = new Grid(33, 33, RoadNetwork.GridSize);
        plane = new Plane(Vector3.up, Vector3.zero);

        roadCreator = new RoadCreator(roadNetwork);
        roadAdjuster = new RoadAdjuster(roadNetwork);
        roadRemover = new RoadRemover(roadNetwork);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if(Input.GetMouseButtonDown(0))
                roadRemover.EditNetwork(GetGridPos(GetMousePosOnPlane()));
            return;
        }
        
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            firstClickPoint = GetGridPos(GetMousePosOnPlane());
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            secondClickPoint = GetGridPos(GetMousePosOnPlane());
            var line = new Line(firstClickPoint, secondClickPoint);

            if (Input.GetMouseButtonUp(0)) 
                roadCreator.EditNetwork(line);
            if (Input.GetMouseButtonUp(1)) 
                roadAdjuster.EditNetwork(line);
        }
    }

    private Vector2Int GetGridPos(Vector3 worldPos)
    {
        var pos = grid.GetGridPositionInt(worldPos);
        return new Vector2Int(pos.x, pos.z);
    }
 
    private Vector3 GetMousePosOnPlane()
    {
        var ray = GetMouseRay(Camera.main);
        return plane.Raycast(ray, out float enter) ? ray.GetPoint(enter) : Vector3.zero;
    }

    private static Ray GetMouseRay(Camera cam) => cam.ScreenPointToRay(Input.mousePosition);

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        grid.DrawGrid();
        roadNetwork.DrawAllGizmos();
    }
}
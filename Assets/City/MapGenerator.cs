using UnityEngine;

namespace City
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject plane;
        [Space]
        [SerializeField] private int scale;
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private int mainBranches;
        [SerializeField] private int subBranches;
        
        public static GridWithObject<CityGridObject> gridWithObject;
    
        public void GenerateMap()
        {
            foreach (Transform tr in transform)
            {
                Destroy(tr.gameObject);
            }
            
            gridWithObject = new GridWithObject<CityGridObject>(width, height, scale, (grid, x, y) =>
            {
                GameObject obj = SpawnObject(plane, transform, new Vector3(x * scale, 0, y * scale), Vector3.one * scale);
                return new CityGridObject(CityGridObject.CityObjectType.Grass, obj, x, y);
            });
            
            GenerationAlgorithms generators = new GenerationAlgorithms();
            generators.BranchAlgorithm(mainBranches, subBranches);
        }
        
        public static GameObject SpawnObject(GameObject gameObject, Transform parent, Vector3 pos, Vector3 scale)
        {
            GameObject go = Object.Instantiate(gameObject, parent, true);
            go.transform.localScale = scale;
            go.transform.position = pos;
            return go;
        }
    }
}

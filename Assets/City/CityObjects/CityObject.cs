using UnityEngine;

namespace City.CityObjects
{
    [System.Serializable]
    public class CityObject
    {
        public GameObject objectToSpawn;

        protected GridWithObject<CityGridObject> gridWithObject;
        protected GameObject SpawnedObject;
        protected CityGridObject Up;
        protected CityGridObject Down;
        protected CityGridObject Right;
        protected CityGridObject Left;

        public virtual bool CanSpawn => true;

        public virtual void Spawn(int x, int y)
        {
            gridWithObject = MapGenerator.gridWithObject;
            SpawnedObject = Object.Instantiate(objectToSpawn, gridWithObject.GetWorldPosition(x, y), Quaternion.identity);
        }

        protected virtual void DeSpawn()
        {
            if (SpawnedObject == null) return;
            Object.Destroy(SpawnedObject);
            SpawnedObject = null;
        }
    }
}
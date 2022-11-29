using UnityEngine;

namespace City.CityObjects
{
    [System.Serializable]
    public class StraightRoad : CityObject
    {
        public override bool CanSpawn =>
            ((Up.IsRoad || Down.IsRoad) && (!Left.IsRoad && !Right.IsRoad)) ||
            ((Right.IsRoad || Left.IsRoad) && (!Up.IsRoad && !Down.IsRoad));

        public override void Spawn(int x, int y)
        {
            gridWithObject = MapGenerator.gridWithObject;
            Up = gridWithObject.GetValue(x, y + 1);
            Down = gridWithObject.GetValue(x, y - 1);
            Right = gridWithObject.GetValue(x + 1, y);
            Left = gridWithObject.GetValue(x - 1, y);

            if (!CanSpawn) return;
            
            base.Spawn(x, y);
            SpawnedObject.transform.eulerAngles = GetRotation();
        }

        protected virtual Vector3 GetRotation()
        {
            if (((Up.IsRoad || Down.IsRoad) && (!Left.IsRoad && !Right.IsRoad)))
                return Vector3.zero;
            return new Vector3(0, 90, 0);
        }
    }
}
using UnityEngine;

namespace City
{
    public class GenerationAlgorithms
    {
        private GridWithObject<CityGridObject> gridWithObject;

        public GenerationAlgorithms()
        {
            gridWithObject = MapGenerator.gridWithObject;
        }

        public void BranchAlgorithm(int mainBranches,int subBranches)
        {
            for (int i = 2; i < gridWithObject.Height - 2; i++)
            {
                gridWithObject.GetValue(gridWithObject.Width / 2, i).ChangeType(CityGridObject.CityObjectType.Road);
            }

            for (int j = 0; j < mainBranches; j++)
            {
                bool horizontal = true;
                Vector2 lastStart1 = new Vector2(gridWithObject.Width / 2, 2);
                Vector2 lastStart2 = new Vector2(gridWithObject.Width / 2, 2);
                Vector2 lastEnd1 = new Vector2(gridWithObject.Width / 2, gridWithObject.Height - 3);
                Vector2 lastEnd2 = new Vector2(gridWithObject.Width / 2, gridWithObject.Height - 3);
                for (int i = 0; i < subBranches; i++)
                {
                    if (horizontal)
                    {
                        GetHorizontalLine(true, lastStart1, lastEnd1, out lastStart1, out lastEnd1);
                        GetHorizontalLine(false, lastStart2, lastEnd2, out lastStart2, out lastEnd2);

                        ChangeHorizontalLineOnGrid(true, CityGridObject.CityObjectType.Road, lastStart1, lastEnd1);
                        ChangeHorizontalLineOnGrid(false, CityGridObject.CityObjectType.Road, lastStart2, lastEnd2);
                    }

                    if (!horizontal)
                    {
                        bool toUp = Random.value > 0.5f;
                        bool toUp2 = Random.value > 0.5f;
                        GetVerticalLine(toUp, lastStart1, lastEnd1, out lastStart1, out lastEnd1);
                        GetVerticalLine(toUp2, lastStart2, lastEnd2, out lastStart2, out lastEnd2);

                        ChangeVerticalLineOnGrid(toUp, CityGridObject.CityObjectType.Road, lastStart1, lastEnd1);
                        ChangeVerticalLineOnGrid(toUp2, CityGridObject.CityObjectType.Road, lastStart2, lastEnd2);
                    }

                    horizontal = !horizontal;
                }
            }
        }

        public void LineLineAlgorithm(int iterationTime)
        {
            bool horizontal = true;
            int lastX1 = 0;
            int lastX2 = 0;
            int lastY1 = 0;
            int lastY2 = 0;

            for (int i = 0; i < iterationTime; i++)
            {
                if (horizontal)
                {
                    int x1 = Random.Range(lastX1, gridWithObject.Width);
                    int x2 = Random.Range(x1, gridWithObject.Width);
                    int y = Random.Range(lastY1, lastY2);

                    for (int x = x1; x < x2; x++)
                    {
                        gridWithObject.GetValue(x, y).ChangeType(CityGridObject.CityObjectType.Road);
                    }

                    lastX1 = x1;
                    lastX2 = x2;
                    lastY1 = y;
                }

                if (!horizontal)
                {
                    int x = Random.Range(lastX1, lastX2);
                    int y1 = Random.Range(0, lastY1);
                    int y2 = Random.Range(y1, gridWithObject.Height);

                    for (int y = y1; y < y2; y++)
                    {
                        gridWithObject.GetValue(x, y).ChangeType(CityGridObject.CityObjectType.Road);
                    }

                    lastX1 = x;
                    lastY1 = y1;
                    lastY2 = y2;
                }

                horizontal = !horizontal;
            }
        }

        private void GetHorizontalLine(bool toRight, Vector2 origin, Vector2 originEnd, out Vector2 start,
            out Vector2 end)
        {
            start = new Vector2(origin.x, Random.Range(origin.y, originEnd.y));
            end = toRight
                ? new Vector2(Random.Range(origin.x + 2, gridWithObject.Width - 2), start.y)
                : new Vector2(Random.Range(1, origin.x - 2), start.y);
        }

        private void GetVerticalLine(bool toUp, Vector2 origin, Vector2 originEnd, out Vector2 start, out Vector2 end)
        {
            start = new Vector2(Random.Range(origin.x, originEnd.x), origin.y);
            end = toUp
                ? new Vector2(start.x, Random.Range(origin.y + 2, gridWithObject.Height - 2))
                : new Vector2(start.x, Random.Range(1, origin.y - 2));
        }

        private void ChangeHorizontalLineOnGrid(bool toRight, CityGridObject.CityObjectType type, Vector2 origin,
            Vector2 destination)
        {
            if (toRight)
                for (int x = (int) origin.x; x < destination.x; x++)
                {
                    gridWithObject.GetValue(x, (int) origin.y).ChangeType(type);
                }
            else
                for (int x = (int) origin.x; x > destination.x; x--)
                {
                    gridWithObject.GetValue(x, (int) origin.y).ChangeType(type);
                }
        }

        private void ChangeVerticalLineOnGrid(bool toUp,CityGridObject.CityObjectType type, Vector2 origin, Vector2 destination)
        {
            if(toUp)
                for (int y = (int) origin.y; y < destination.y; y++)
                {
                    gridWithObject.GetValue((int) origin.x, y).ChangeType(type);
                }
            else
                for (int y = (int) origin.y; y > destination.y; y--)
                {
                    gridWithObject.GetValue((int) origin.x, y).ChangeType(type);
                }
        }
    }
}
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class LineTests
    {
        [Test]
        public void TestIntersections()
        {
            // Use the Assert class to test conditions.
            TestConnectedLineIntersections();
            TestDisconnectedLinesIntersections();
        }

        private void TestConnectedLineIntersections()
        {
            TestLinesIntersection(Vector2.zero, true, 
                new Line(new Vector2Int(-1, 0), new Vector2Int(1, 0)),
                new Line(new Vector2Int(0, 1), new Vector2Int(0, -1)));
            
            TestLinesIntersection(Vector2.zero, true, 
                new Line(new Vector2Int(1, 0), new Vector2Int(-1, 0)),
                new Line(new Vector2Int(0, -1), new Vector2Int(0, 1)));
        }
        

        private void TestDisconnectedLinesIntersections()
        {
            TestLinesIntersection(Vector2.zero, false, 
                new Line(new Vector2Int(1, 1), new Vector2Int(2, 2)),
                new Line(new Vector2Int(-1, -1), new Vector2Int(-2, -2)));
        }

        private void TestLinesIntersection(Vector2 expected ,bool intersected ,params Line[] lines)
        {
            for (int i = 0; i < lines.Length - 1; i++)
                TestLineIntersection(lines[i], lines[i + 1], intersected, expected);

            for (int i = lines.Length; i < 1; i--) 
                TestLineIntersection(lines[i], lines[i - 1], intersected, expected);
        }


        private void TestLineIntersection(Line line1, Line line2, bool intersected, Vector2 expected = default)
        {
            if(intersected)
            {
                Assert.IsTrue(LineExtensions.LineLineIntersection(line1, line2, out var intersection));
                Assert.IsTrue(intersection == expected);
            }
            else
            {
                Assert.IsFalse(LineExtensions.LineLineIntersection(line1, line2, out _));
            }
        }
    }
}
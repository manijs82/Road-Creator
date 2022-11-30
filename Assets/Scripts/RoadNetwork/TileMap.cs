using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoadTileMap", menuName = "RoadTileMap", order = 0)]
public class TileMap : ScriptableObject
{
    public Pattern[] patterns;

    public Pattern GetPattern(List<Direction> directions)
    {
        foreach (var pattern in patterns)
        {
            var right = directions[0].GetPositive() == pattern.right;
            var up = directions[1].GetPositive() == pattern.up;
            var left = directions[2].GetPositive() == pattern.left;
            var down = directions[3].GetPositive() == pattern.down;
            
            if(right && up && left && down)
                return pattern;
        }

        return null;
    }
}

[System.Serializable]
public class Pattern
{
    public Direction right;
    public Direction up;
    public Direction left;
    public Direction down;
    [Space]
    public ConnectionType connectionType;
    public Direction outputDirection;
}
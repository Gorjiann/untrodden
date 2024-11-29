using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(fileName = "RoomData", menuName = "Scriptable Objects/RoomData")]
public class RoomData : ScriptableObject
{
    public int RoomIndex;
    public TileBase WallTile;
    public Sprite WallSprite;
    public bool[] AllowedDirections;
}

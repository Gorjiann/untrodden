using UnityEngine;

using System.Collections.Generic;

[CreateAssetMenu(fileName = "RoomsContainer", menuName = "Scriptable Objects/RoomsContainer")]
public class RoomsContainer : ScriptableObject
{
    public List< RoomData> AllRooms;
    public Sprite  [] RoomSprites;
}

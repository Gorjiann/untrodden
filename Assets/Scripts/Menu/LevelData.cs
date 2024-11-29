using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    public int[] RoomsSpawnrateData;
    public bool [] RoomDescription;
    public bool[] EnemiesDescription;
    public string GoalDescription;
}

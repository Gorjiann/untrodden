using UnityEngine;

public class LevelGlobalPreference : MonoBehaviour
{
    public static int CharacterIndex;
    public static int LevelIndex;
    public static int DifficultyMultiply;
    public static int Size;
    public static bool IsEndlees;
    public static int [] RoomsSpawnrate = new int[8];
    public static int [] ItemCounts = new int[5];
    public static bool [] EnemySpawn = new bool[6];
}

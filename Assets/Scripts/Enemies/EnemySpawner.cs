using UnityEngine;
using System.Collections.Generic;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyData[] AllEnemies;
    [SerializeField] private EnemyStats EnemyPrefab;
    [SerializeField] private int Width, Height;
    [SerializeField] private AgressionController AgressionController;
    [SerializeField] private DungeonGenerator Generator;
    private void Awake()
    {
        AgressionController = GetComponent<AgressionController>();
    }
    public void SpawnEnemy(int x, int y)
    {
        int chance = Random.Range(0,EnemyToSpawn().Count);
        EnemyStats enemy = Instantiate(EnemyPrefab, new Vector2(x, y), Quaternion.identity);
        enemy.AgressionController = AgressionController;
        enemy.Generator = Generator;
        enemy.Initialize(GetComponent<DungeonGenerator>().Player.transform, new Vector2Int(x, y), EnemyToSpawn()[chance]);

        Width = x;
        Height = y;
    }
    public List<EnemyData> EnemyToSpawn()
    {

        List<EnemyData> enemytospawn = new List<EnemyData>();
        for (int i = 0; i < LevelGlobalPreference.EnemySpawn.Length; i++)
        {
            if (LevelGlobalPreference.EnemySpawn[i])
                enemytospawn.Add(AllEnemies[i]);
        }
        return enemytospawn;
    }
    public void PassiveSpawner(int spawcyk)
    {
        int chance = Random.Range(0, EnemyToSpawn().Count);
            int randomX = Random.Range(2, Width - 2);
            int randomY = Random.Range(2, Height - 2);

        for (int i = 0; i <= spawcyk; i++)
        {
            EnemyStats enemy = Instantiate(EnemyPrefab, new Vector2(randomX, randomY), Quaternion.identity);
            enemy.Initialize(GetComponent<DungeonGenerator>().Player.transform, new Vector2Int(randomX, randomY), EnemyToSpawn()[chance]);
            enemy.AgressionController = AgressionController;
            enemy.Generator = Generator;
        }
            
    }
}

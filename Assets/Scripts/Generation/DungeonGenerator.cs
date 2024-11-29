using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private Room RoomPrefab;
    [SerializeField] private BoxCollider2D CameraZone;
    [SerializeField] public GameObject Player;
    [SerializeField] private Torch Torch;
    [SerializeField] private Treasures Treasure;
    [SerializeField] private Exit Exit;
    [SerializeField] public int Width, Height;
    [SerializeField] private List<Transform> AllLights = new List<Transform>();
    [SerializeField] private Tilemap tilemap;
    [SerializeField] public Room[,] SpawnedRooms;
    private EnemySpawner EnemySpawner;
    private void Awake()
    {
        EnemySpawner = GetComponent<EnemySpawner>();    
        SpawnGrid();
        EventManager.OnTurnPassed.AddListener(UpdateGrid);
        EventManager.OnLightAdded.AddListener(AddLightings);
        EventManager.OnLightRemoved.AddListener(RemoveLightings);
        SpawnTorches();
        SpawnEnemies();
        SpawnTreasure();    
    }
    private void UpdateGrid()
    {
        bool[,] isRoomLit = new bool[Width, Height];
        AllLights.RemoveAll(light => light == null);
        foreach (Transform lightPos in AllLights)
        {
            int lightX = Mathf.Clamp(Mathf.RoundToInt(lightPos.position.x), 0, Width - 1);
            int lightY = Mathf.Clamp(Mathf.RoundToInt(lightPos.position.y), 0, Height - 1);
            isRoomLit[lightX, lightY] = true;
        }
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (!isRoomLit[x, y])
                {
                    SpawnedRooms[x, y].EntangleTile();
                }
            }
        }
    }
    public void SpawnTreasure()
    {
        Vector2 playerPosition = Player.transform.position;

        float randomX;
        float randomY;

        do
        {
            randomX = Random.Range((int)-Width/5, (int)-Width/4) + playerPosition.x;
            if (Random.Range(0, 2) == 1)
            {
                randomX = Random.Range((int)Width / 4, (int)Width / 5) + playerPosition.x;
            }
            randomY = Random.Range((int)-Height / 5, (int)-Height / 4) + playerPosition.y;
            if (Random.Range(0, 2) == 1)
            {
                randomY = Random.Range((int)Height / 5, (int)Height / 4) + playerPosition.y;
            }
        }
        while (randomX < 2 || randomX >= Width-2 || randomY < 2 || randomY >= Height-2);

        Vector2 randomPos = new Vector2(randomX, randomY);
        SpawnedRooms[(int)randomX, (int)randomY].CollapseAsStart();
        Treasures treasurenew = Instantiate(Treasure, randomPos, Quaternion.identity);
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < (Width + Height) / 10 - 2; i++)
        {
            int randomX = Random.Range(2, Width - 2);
            int randomY = Random.Range(2, Height - 2);
            EnemySpawner.SpawnEnemy(randomX, randomY);
        }
    }
    public void SpawnExit()
    {
        Vector2 playerPosition = Player.transform.position;

        float randomX;
        float randomY;

        do
        {
            randomX = Random.Range((int)-Width / 5, (int)-Width / 4) + playerPosition.x;
            if (Random.Range(0, 2) == 1)
            {
                randomX = Random.Range((int) -Width / 5, (int) -Width / 4) + playerPosition.x;
            }
            randomY = Random.Range((int)-Height / 5, (int)-Height / 4) + playerPosition.y;
            if (Random.Range(0, 2) == 1)
            {
                randomY = Random.Range((int)Height / 5, (int)Height / 4) + playerPosition.y;
            }
        }
        while (randomX < 2 || randomX >= Width - 2 || randomY < 2 || randomY >= Height - 2);

        Vector2 randomPos = new Vector2(randomX, randomY);
        SpawnedRooms[(int)randomX, (int)randomY].CollapseAsStart();
        Exit newexit = Instantiate(Exit, randomPos, Quaternion.identity);
    }
    public void UpdateTileInTilemap(int x, int y, RoomData roomData)
    {
        var tilePosition = new Vector3Int(x, y, 0);
        var tile = Instantiate(roomData.WallTile);
        tilemap.SetTile(tilePosition, tile);
        tilemap.GetComponent<ShadowCasterGenerator>().OnTilemapUpdated();
    }
    public void ClearTileInTilemap(int x, int y)
    {
        var tilePosition = new Vector3Int(x, y, 0);
        tilemap.SetTile(tilePosition, null);
        tilemap.GetComponent<ShadowCasterGenerator>().OnTilemapUpdated();
    }
    private void AddLightings(Transform light) => AllLights.Add(light);
    private void RemoveLightings(Transform light)
    {
        AllLights.Remove(light);
        AllLights.RemoveAll(l => l == null);
    }
    private void SpawnGrid()
    {
        SpawnedRooms = new Room[Width, Height];
        Vector2 center = new Vector2(Width / 2, Height / 2);
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                var newRoom = Instantiate(RoomPrefab, new Vector2(x, y), Quaternion.identity);
                newRoom.Spawner = this;
                SpawnedRooms[x, y] = newRoom;
            }
        }
        Player.transform.position = center;
        CameraZone.transform.position = center;
        CameraZone.size = new Vector2(Width, Height);
        Player.SetActive(true);
        SpawnedRooms[Width / 2, Height / 2].CollapseAsStart();
    }
    private void SpawnTorches()
    {
        for (int i = 0; i < (Width + Height) / 10 + 2; i++)
        {
            int randomX = Random.Range(2, Width - 2);
            int randomY = Random.Range(2, Height - 2);
            Vector2 randomPos = new Vector2(randomX, randomY);
            SpawnedRooms[randomX, randomY].CollapseTile();
            Torch newtorch = Instantiate(Torch, randomPos, Quaternion.identity);
            newtorch.IsEndless = true;
            newtorch.Lifetime = 1;
        }
    }
}
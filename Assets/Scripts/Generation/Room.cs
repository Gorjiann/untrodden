using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Room : MonoBehaviour, IDamager
{
    public MonoBehaviour MonoRef => this;
    [SerializeField] public bool IsCollapsed;
    [SerializeField] public bool IsLighted;
    [SerializeField] private RoomsContainer RoomsStorage;
    [SerializeField] public bool[] Directions = new bool[4];
    private SpriteRenderer SelfSprite;
    [SerializeField] private Sprite ActiveSprite;
    private BoxCollider2D selfCollider;
    [SerializeField] public DungeonGenerator Spawner;
    [SerializeField] private int RoomType;
    [SerializeField] private Treasures selftreasure;
    [SerializeField] private Trap selftrap;
    [SerializeField] private Altar selfaltar;
    public GameObject temporalobject;
    public RoomData currentRoomData;
    private bool EffectActivated;


    public int Damage { get; set; } = 1;  
    private void Awake()
    {
        selfCollider = GetComponent<BoxCollider2D>();
        SelfSprite = GetComponent<SpriteRenderer>();
        InitializeParameters(RoomsStorage.AllRooms[0]);
    }
    public SpriteRenderer ReturnSprite() => SelfSprite;
    public void InitializeParameters(RoomData roomInfo)
    {
        currentRoomData = roomInfo;
        roomInfo.AllowedDirections.CopyTo(Directions, 0);
        selfCollider.enabled = IsCollapsed;
    }
    public void CollapseAsStart()
    {
        IsCollapsed = true;
        RoomType = SetRoomType();
        SelfSprite.sprite = RoomsStorage.RoomSprites[0];
        InitializeParameters(RoomsStorage.AllRooms[1]);
        Spawner.UpdateTileInTilemap(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), currentRoomData);
    }
    public void EntangleTile()
    {
        if (!IsCollapsed) return;
        IsCollapsed = false;
        SelfSprite.sprite = null;
        InitializeParameters(RoomsStorage.AllRooms[0]);
        Spawner.ClearTileInTilemap(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Destroy(temporalobject);
    }
    public void CollapseTile()
    {
        if (IsCollapsed) return;
        EffectActivated = false;
        RoomType = SetRoomType();
        Vector2Int position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Room topRoom = GetNeighborRoom(position.x, position.y + 1);
        Room downRoom = GetNeighborRoom(position.x, position.y - 1);
        Room rightRoom = GetNeighborRoom(position.x + 1, position.y);
        Room leftRoom = GetNeighborRoom(position.x - 1, position.y);
        bool[] significantDirections = {
topRoom != null && topRoom.IsCollapsed,
rightRoom != null && rightRoom.IsCollapsed,
downRoom != null && downRoom.IsCollapsed,
leftRoom != null && leftRoom.IsCollapsed
};
        bool canCorners = CanCorners();
        IEnumerable<RoomData> filteredRooms = canCorners
        ? RoomsStorage.AllRooms
        : RoomsStorage.AllRooms.SkipLast(4);
        var potentialRooms = filteredRooms
        .Where(room =>
        (!significantDirections[0] || room.AllowedDirections[0] == topRoom.Directions[2]) &&
        (!significantDirections[1] || room.AllowedDirections[1] == rightRoom.Directions[3]) &&
        (!significantDirections[2] || room.AllowedDirections[2] == downRoom.Directions[0]) &&
        (!significantDirections[3] || room.AllowedDirections[3] == leftRoom.Directions[1])
        )
        .ToList();
        if (potentialRooms.Count > 0)
        {
            RoomData selectedRoom = potentialRooms[Random.Range(0, potentialRooms.Count)];
            IsCollapsed = true;
            InitializeParameters(selectedRoom);
            SelfSprite.sprite = RoomsStorage.RoomSprites[RoomType];
            Spawner.UpdateTileInTilemap(position.x, position.y, selectedRoom);
            SpawnObj();
        }
    }
    private void SpawnObj()
    {
        if (RoomType == 3)
        {
            int chanse1 = Random.Range(0, 100);
            int chanse2 = Random.Range(0, 2);
            if (chanse1 <= 50)
            {
                if (chanse2 == 1)
                {
                    GameObject newtreas = Instantiate(selftreasure.gameObject, transform.position, Quaternion.identity);
                    temporalobject = newtreas;
                }
                else
                {
                    GameObject newtrap = Instantiate(selftrap.gameObject, transform.position, Quaternion.identity);
                    temporalobject = newtrap;
                }
            }
        }
        else if (RoomType == 7)
        {
            int chanse1 = Random.Range(0, 100);
            if (chanse1 <= 80)
            {
                    GameObject newtreas = Instantiate(selfaltar.gameObject, transform.position, Quaternion.identity);
                    temporalobject = newtreas;
            }
        }
        else
        {
            int chanse1 = Random.Range(0, 100);
            int chanse2 = Random.Range(0, 2);
            if (currentRoomData.RoomIndex > RoomsStorage.AllRooms.Count - 4)
            {
                if (chanse1 <= 30)
                {
                    if (chanse2 == 1)
                    {
                        GameObject newtreas = Instantiate(selftreasure.gameObject, transform.position, Quaternion.identity);
                        temporalobject = newtreas;
                    }
                    else
                    {
                        GameObject newtrap = Instantiate(selftrap.gameObject, transform.position, Quaternion.identity);
                        temporalobject = newtrap;
                    }
                }
            }
            else
            {
                if (chanse1 <= 7)
                {
                    if (chanse2 == 1)
                    {
                        GameObject newtreas = Instantiate(selftreasure.gameObject, transform.position, Quaternion.identity);
                        temporalobject = newtreas;
                    }
                    else
                    {
                        GameObject newtrap = Instantiate(selftrap.gameObject, transform.position, Quaternion.identity);
                        temporalobject = newtrap;
                    }
                }
            }

        }
    }
    private Room GetNeighborRoom(int x, int y) => IsValidPosition(x, y) ? Spawner.SpawnedRooms[x, y] : null;
    private bool CanCorners() => RoomType != 1;
    private bool IsValidPosition(int x, int y) =>
    x >= 0 && x < Spawner.SpawnedRooms.GetLength(0) && y >= 0 && y < Spawner.SpawnedRooms.GetLength(1);
    public int SetRoomType()
    {
        int roomTypeWeight = LevelGlobalPreference.RoomsSpawnrate.Sum();
        if (roomTypeWeight == 0)
        {
            return 0;
        }
        int randomValue = Random.Range(0, roomTypeWeight);
        int cumulativeWeight = 0;
        for (int i = 0; i < LevelGlobalPreference.RoomsSpawnrate.Length; i++)
        {
            cumulativeWeight += LevelGlobalPreference.RoomsSpawnrate[i];
            if (randomValue < cumulativeWeight)
            {
                return i;
            }
        }
        return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>() != null &&!EffectActivated)
        {
            EventManager.ChangeMagnetState(true);
            switch (RoomType)
            {
                case 2:
                    EventManager.ChangeMagnetState(false);
                    break;
                case 4:
                    collision.GetComponent<PlayerCharacterController>().CurrentCharacter.GetCharacter(collision.gameObject).OnTakeDamege(this);
                    break;
                case 5:
                    EventManager.AgressionUpdate(1);
                    break;
                case 6:
                    EventManager.AgressionUpdate(-1);
                    break;
            }
            EffectActivated = true;
        }
    }
    public void CollapseNeighbor(bool[] direction)
    {
        Vector2Int position = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        Room topRoom = GetNeighborRoom(position.x, position.y + 1);
        Room downRoom = GetNeighborRoom(position.x, position.y - 1);
        Room rightRoom = GetNeighborRoom(position.x + 1, position.y);
        Room leftRoom = GetNeighborRoom(position.x - 1, position.y);
        if (direction[0] && Directions[0]) topRoom?.CollapseTile();
        if (direction[1] && Directions[1]) rightRoom?.CollapseTile();
        if (direction[2] && Directions[2]) downRoom?.CollapseTile();
        if (direction[3] && Directions[3]) leftRoom?.CollapseTile();
    }
}
using System.Collections.Generic;
using UnityEngine;
public enum EnemyState
{
    Peaceful,
    Pursuing
}
public class EnemyStats : MonoBehaviour
{
    private EnemyState state = EnemyState.Peaceful;
    private EnemyMovement movement;

    [SerializeField] private int CurrentHealth;
    [SerializeField] private EnemyData ThisEnemy;
    [SerializeField] private int AggressionThreshold;
    [SerializeField] private int ChaseTimer;
    [SerializeField] private int CurrentSpeed;
    [SerializeField] private Material litmat;
    [SerializeField] private Material unlitmat;
    public Transform playerTarget;
    private bool PlayerInvis;
    private Vector2Int gridPosition;
    public AgressionController AgressionController;
    public DungeonGenerator Generator;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        EventManager.OnTurnPassed.AddListener(DoAction);
        EventManager.OnInvisabilityChange.AddListener(UpdateIvisability);
    }
    private void UpdateIvisability(bool fg) => PlayerInvis = fg;
    public void Initialize(Transform player, Vector2Int initialPosition, EnemyData data)
    {
        if(data.IsLighted)
        GetComponent<SpriteRenderer>().material = litmat;
        else GetComponent<SpriteRenderer>().material = unlitmat;


        CurrentHealth = data.MaxHealhData;
        AggressionThreshold = data.AgressionThereshold;
        ChaseTimer = 0;
        GetComponent<SpriteRenderer>().sprite = data.EnemySprite;
        playerTarget = player;
        gridPosition = initialPosition;
        CurrentSpeed = data.MovementSpeed;
        ThisEnemy = data;
        movement = GetComponent<EnemyMovement>();
        movement.Initialize(initialPosition, Generator);

        ThisEnemy.GetEnemy(this).OnSpawn();
    }

    private void UpdateState()
    {
        float distanceToPlayer = Vector2.Distance(movement.GridPosition, (Vector2)playerTarget.position);

        switch (state)
        {
            case EnemyState.Peaceful:
                HandlePeacefulState(distanceToPlayer);
                break;

            case EnemyState.Pursuing:
                if (PlayerInvis)
                {
                    ChangeState(EnemyState.Peaceful);
                    return;
                }
                Vector2Int playerPosition = new Vector2Int((int)playerTarget.position.x, (int)playerTarget.position.y);
                HashSet<Vector2Int> obstacles = GetObstacles();
                movement.MoveTo(playerPosition, obstacles);
                if (IsPlayerInRange())
                {
                    Attack();
                }
                break;

        }
        ThisEnemy.GetEnemy(this).OnSpecialAction();
    }
    private HashSet<Vector2Int> GetObstacles()
    {
        HashSet<Vector2Int> obstacles = new HashSet<Vector2Int>();
        for (int x = 0; x < Generator.Width; x++)
        {
            for (int y = 0; y < Generator.Height; y++)
            {
                Room room = Generator.SpawnedRooms[x, y];
                if (room != null && !room.IsCollapsed)
                {
                    obstacles.Add(new Vector2Int(x, y));
                }
            }
        }
        return obstacles;
    }

    private void HandlePeacefulState(float distanceToPlayer)
    {
        if (distanceToPlayer <= 4)
        {
            ChangeState(EnemyState.Pursuing);
        }
        else if (AgressionController.ReturnAgression() >= AggressionThreshold)
        {
            float chance = Random.Range(0f, 100f);
            if (chance < AgressionController.ReturnAgression() / 3 && !PlayerInvis)
            {
                if (state != EnemyState.Pursuing)
                {
                    //EventManager.SendGameMessage("Enemy becomes aggressive!");
                }
                ChangeState(EnemyState.Pursuing);
            }
        }
    }

    private void HandlePursuingState(float distanceToPlayer)
    {
        if (distanceToPlayer <= 3 && !PlayerInvis)
        {
            ChaseTimer = 30;
        }
        else if (--ChaseTimer <= 0)
        {
            ChangeState(EnemyState.Peaceful);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        state = newState;
        ChaseTimer = state == EnemyState.Pursuing ? 30 : 0;
    }

    public void DoAction()
    {
        CurrentSpeed--;
        if (CurrentSpeed == 0)
        {
            UpdateState();
            if (!PlayerInvis)
            {
                switch (state)
                {
                    case EnemyState.Peaceful:
                        movement.MoveRandomly();
                        break;

                    case EnemyState.Pursuing:
                        Vector2Int playerPosition = new Vector2Int((int)playerTarget.position.x, (int)playerTarget.position.y);
                        
                        if (IsPlayerInRange())
                        {
                            Attack();
                        }
                        else movement.MoveTo(playerPosition, new HashSet<Vector2Int>());
                        break;
                }
            }
            CurrentSpeed = ThisEnemy.MovementSpeed;
        }

    }
    private bool IsPlayerInRange()
    {
        return Vector2.Distance(transform.position, playerTarget.position) <= 0.8;
    }

    private void Attack()
    {
        ThisEnemy.GetEnemy(this).OnAttack(playerTarget.gameObject);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            ThisEnemy.GetEnemy(this).OnDie();
        }
    }
}

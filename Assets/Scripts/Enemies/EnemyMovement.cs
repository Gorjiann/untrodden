using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }
    public DungeonGenerator Generator;
    private void Awake()
    {
        GridPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
    }

    public void Initialize(Vector2Int initialPosition, DungeonGenerator generator)
    {
        Generator = generator;
        GridPosition = initialPosition;
        transform.position = new Vector3(GridPosition.x, GridPosition.y, 0);
    }

    public void MoveRandomly()
    {
        Vector2Int randomDirection = new Vector2Int(Random.Range(-1, 2), Random.Range(-1, 2));
        GridPosition += randomDirection;
        UpdatePosition();
    }

    public void MoveTo(Vector2Int targetPosition, HashSet<Vector2Int> obstacles)
    {
        Vector2Int nextStep = FindPath(GridPosition, targetPosition, obstacles);
        if (nextStep != GridPosition)
        {
            GridPosition = nextStep;
            UpdatePosition();
        }
    }


    private void UpdatePosition()
    {
        transform.position = new Vector3(GridPosition.x, GridPosition.y, 0);
    }

    private Vector2Int FindPath(Vector2Int start, Vector2Int goal, HashSet<Vector2Int> obstacles)
    {
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
        PriorityQueue<Vector2Int> openSet = new PriorityQueue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, int> gScore = new Dictionary<Vector2Int, int>();
        Dictionary<Vector2Int, int> fScore = new Dictionary<Vector2Int, int>();

        openSet.Enqueue(start, 0);
        gScore[start] = 0;
        fScore[start] = GetManhattanDistance(start, goal);

        while (openSet.Count > 0)
        {
            Vector2Int current = openSet.Dequeue();

            if (current == goal)
            {
                return ReconstructPath(cameFrom, current);
            }

            closedSet.Add(current);

            foreach (Vector2Int neighbor in GetNeighbors(current, obstacles))
            {
                if (closedSet.Contains(neighbor)) continue;

                int tentativeGScore = gScore[current] + 1;

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + GetManhattanDistance(neighbor, goal);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }
        }
        return start;
    }

    private IEnumerable<Vector2Int> GetNeighbors(Vector2Int current, HashSet<Vector2Int> obstacles)
    {
        Vector2Int[] directions =
        {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2Int neighbor = current + directions[i];
            if (IsValidMove(current, neighbor, i, obstacles))
            {
                yield return neighbor;
            }
        }
    }

    private bool IsValidMove(Vector2Int current, Vector2Int neighbor, int directionIndex, HashSet<Vector2Int> obstacles)
    {
        Room currentRoom = GetRoomAt(current);
        Room neighborRoom = GetRoomAt(neighbor);

        return currentRoom != null &&
               neighborRoom != null &&
               currentRoom.Directions[directionIndex] &&
               neighborRoom.Directions[(directionIndex + 2) % 4] &&
               !obstacles.Contains(neighbor);
    }

    private Room GetRoomAt(Vector2Int position)
    {
        if (Generator.SpawnedRooms != null &&
            position.x >= 0 && position.x < Generator.Width &&
            position.y >= 0 && position.y < Generator.Height)
        {
            return Generator.SpawnedRooms[position.x, position.y];
        }
        return null;
    }

    private Vector2Int ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        while (cameFrom.ContainsKey(current) && cameFrom[current] != GridPosition)
        {
            current = cameFrom[current];
        }
        return current;
    }
    private int GetManhattanDistance(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
}

using UnityEngine;

public class AgressionController : MonoBehaviour
{
   [SerializeField] private int CurrentAggression;
   [SerializeField] private int DifficultraisParameter;
   [SerializeField] private float DifficultyMultiplyer;
    [SerializeField] private int maxAggression = 100;
    private EnemySpawner EnemySpawner;
    private int count;

    private void Awake()
    {
        EventManager.OnAgressionChange.AddListener(ChangeAgression);
        EventManager.OnTurnPassed.AddListener(RaiseDifficult);
        EnemySpawner = GetComponent<EnemySpawner>();
        count = (int) (DifficultraisParameter / DifficultyMultiplyer);
    }
    public int ReturnAgression() => CurrentAggression;
    public void ChangeAgression(int amount)
    {
        CurrentAggression += amount;
        if (CurrentAggression < 0f)
        {
            CurrentAggression = 0;
        }
        else if (CurrentAggression > maxAggression)
        {
            CurrentAggression = maxAggression;
        }
    }

    public void RaiseDifficult()
    {
        count--;
        int returnspawncyk = (int)(CurrentAggression / 30 + 1);
        if(count <= 0)
        {
            ChangeAgression(2);
            EnemySpawner.PassiveSpawner(returnspawncyk);
            count = (int)(DifficultraisParameter / DifficultyMultiplyer);
        }
    }
}
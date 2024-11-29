using UnityEngine;
[CreateAssetMenu(fileName = "Hunter", menuName = "Enemies/Hunter")]
public class HunterData : EnemyData
{
    public EnemyStats Stats;
    public EnemyData EnemyData;
    public Trap Trap;
    public override EnemyBase GetEnemy(EnemyStats enemy)
    {
        return new Hunter(enemy, MaxHealhData, DamageData, AgressionThereshold, MovementSpeed, Stats, EnemyData, Trap,  DeathSound);
    }
}


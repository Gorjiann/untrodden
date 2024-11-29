using UnityEngine;
[CreateAssetMenu(fileName = "Rat", menuName = "Enemies/Rat")]
public class RatData : EnemyData
{
    public override EnemyBase GetEnemy(EnemyStats enemy)
    {
        return new Rat(enemy, MaxHealhData, DamageData, AgressionThereshold, MovementSpeed, DeathSound);
    }
}

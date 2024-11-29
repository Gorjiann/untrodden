using UnityEngine;
[CreateAssetMenu(fileName = "Beast", menuName = "Enemies/Beast")]
public class BeastData : EnemyData
{
    public override EnemyBase GetEnemy(EnemyStats enemy)
    {
        return new Beast(enemy, MaxHealhData, DamageData, AgressionThereshold, MovementSpeed, DeathSound);
    }
}

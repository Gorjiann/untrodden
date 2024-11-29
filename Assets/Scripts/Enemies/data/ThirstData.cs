using UnityEngine;
[CreateAssetMenu(fileName = "Thirst", menuName = "Enemies/Thirst")]
public class ThirstData : EnemyData
{
    public override EnemyBase GetEnemy(EnemyStats enemy)
    {
        return new Thirst(enemy, MaxHealhData, DamageData, AgressionThereshold, MovementSpeed, DeathSound);
    }
}

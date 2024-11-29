using UnityEngine;
[CreateAssetMenu(fileName = "Skeleton", menuName = "Enemies/Skeleton")]
public class SkeletonData : EnemyData
{
    public override EnemyBase GetEnemy(EnemyStats enemy)
    {
        return new Skeleton(enemy, MaxHealhData, DamageData, AgressionThereshold, MovementSpeed, DeathSound);
    }
}

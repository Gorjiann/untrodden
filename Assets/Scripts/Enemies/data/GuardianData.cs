using UnityEngine;
[CreateAssetMenu(fileName = "Guardian", menuName = "Enemies/Guardian")]
public class GuardianData : EnemyData
{
    public Torch Torch;
    public override EnemyBase GetEnemy(EnemyStats enemy)
    {
        return new Guardian(enemy, MaxHealhData, DamageData, AgressionThereshold, MovementSpeed, Torch, DeathSound);
    }
}

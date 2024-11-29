using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    public abstract EnemyBase GetEnemy(EnemyStats enemy);

    public int Index;
    public AudioClip DeathSound;
    public int MaxHealhData;
    public int DamageData;
    public int MovementSpeed;
    public int AgressionThereshold;
    public bool IsLighted;
    public Sprite EnemySprite;
}

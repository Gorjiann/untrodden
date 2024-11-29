using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamager
{
    public MonoBehaviour MonoRef => this;
    public Transform PlayerTarget;
    public Vector2Int gridPosition;
    public int MaxHealth;
    public int Damage {get;set;}
    public int Speed;
    public int AggressionThreshold;
    public int detectionRadius;
    public int attackRange;
    public AgressionController agrospawn;
    public int TurnsCounts;
    public EnemyStats thisenemy;
    public AudioClip Dieclip;
    public EnemyBase(EnemyStats enemy,int maxhp, int damage, int agressionpass, int speed, AudioClip deathclip )
    {
        thisenemy = enemy;
        MaxHealth = maxhp;
        Damage = damage;
        Speed = speed;
        TurnsCounts = speed;
        Dieclip = deathclip;
    }
    public virtual void OnAttack(GameObject player) 
    {
    player.GetComponent<PlayerStats>().TakeDamage(Damage);
    }
    public virtual void OnTakeDamage() { }
    public virtual void OnDie() 
    {
        Destroy(thisenemy.gameObject);
        EventManager.DieSound(Dieclip);
    }
    public abstract void OnSpecialAction();
    public virtual void OnSpawn() { }
}

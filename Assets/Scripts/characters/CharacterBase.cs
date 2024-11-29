using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class CharacterBase : MonoBehaviour
{
    public int MaxHealth;
    public int PlayerDamage;
    public PlayerStats player;
    public CharacterBase(GameObject getplayer, int maxhp, int damage)
    {
        MaxHealth = maxhp;
        PlayerDamage = damage;
        player = getplayer.GetComponent<PlayerStats>();
    }
    public virtual void OnAtack()
    {
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(player.transform.position, 1.1f);

        foreach (Collider2D collider in Enemies)
        {
            EnemyStats enemy = collider.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(PlayerDamage);
                EventManager.PassTheTurn();
            }
        }
    }
    public virtual void OnTakeTreasure() { }
    public virtual void OnTurn() { }
    public virtual void OnTakeDamege(IDamager atacker)
    {
        player.TakeDamage(atacker.Damage);
    }
    public virtual void OnTakeHealth(int health)
    {
        player.TakeHealth(health);
    }
    public abstract void OnStart();
}

using UnityEngine;

public class Blacksmith : CharacterBase
{
    public Blacksmith(GameObject getplayer, int maxhp, int damage)
    : base(getplayer, maxhp, damage)
    {

    }
    public override void OnAtack() {
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(player.transform.position, 1.1f);

        foreach (Collider2D collider in Enemies)
        {

            Vector3 direction = collider.transform.position - player.transform.position;
            Vector3 normalizedDirection = direction.normalized* 3;
            EnemyStats enemy = collider.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.transform.position += normalizedDirection;
                enemy.TakeDamage(PlayerDamage);
            }
        }
        player.TakeDamage(1);
    }
    public override void OnTakeDamege(IDamager atacker)
    {
        EnemyStats enemy = atacker.MonoRef.GetComponent<EnemyStats>();
        player.TakeDamage(atacker.Damage);

        if (enemy != null)
        {
            enemy.TakeDamage((int)atacker.Damage);
        }
    }
    public override void OnStart() { }
}

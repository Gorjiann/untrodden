using UnityEngine;

public class Psychic : CharacterBase
{
    public Psychic(GameObject getplayer, int maxhp, int damage)
    : base(getplayer, maxhp, damage)
    {

    }
    public override void OnAtack()
    {
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(player.transform.position, 4f);

        foreach (Collider2D collider in Enemies)
        {
            EnemyStats enemy = collider.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(PlayerDamage);
            }
        }
    }
    public override void OnStart() {
        player.GetComponent<PlayerInventory>().ItemsCount[3] += 5;
    }
}

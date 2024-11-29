using UnityEngine;
using System.Collections.Generic;
public class Paramedic : CharacterBase
{
    private Transform playerpos;
    public Paramedic(GameObject getplayer, int maxhp, int damage)
    : base(getplayer, maxhp, damage)
    {
        playerpos = getplayer.transform;
    }
    public override void OnTakeTreasure()
    {
        player.TakeHealth(1);
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(playerpos.position, 3f);
        if (Enemies.Length > 0)
        {
            foreach (Collider2D collider in Enemies)
            {
                EnemyStats enemy = collider.GetComponent<EnemyStats>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                }
            }
        }
    }

    public override void OnTakeHealth(int health)
    {
        player.TakeHealth(health * 2);

        Collider2D[] Enemies = Physics2D.OverlapCircleAll(playerpos.position, 3f);

        if (Enemies.Length > 0)
        {
            foreach (Collider2D collider in Enemies)
            {
                EnemyStats enemy = collider.GetComponent<EnemyStats>();
                if (enemy != null)
                {
                    enemy.TakeDamage(health / 2 + 1);
                }
            }
        }
    }


    public override void OnStart() { }
}

using UnityEngine;

public class Undead : CharacterBase
{
    private int CurrentTurns{get;set;}
    private int MaxRevailTurns;
    public Undead(GameObject getplayer, int maxhp, int damage, int turnstorevial)
    : base(getplayer, maxhp, damage)
    {
        CurrentTurns = 0;
        MaxRevailTurns = turnstorevial;
    }
    public override void OnTurn()
    {

    }

    public override void OnTakeHealth(int health) {
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(player.transform.position, 2f);

        foreach (Collider2D collider in Enemies)
        {
            EnemyStats enemy = collider.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(health);
            }
        }
    }
    public override void OnStart() { }
}

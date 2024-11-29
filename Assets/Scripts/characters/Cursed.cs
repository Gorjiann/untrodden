using UnityEngine;

public class Cursed : CharacterBase
{
    private int CurseLevel;
    public Cursed(GameObject getplayer, int maxhp, int damage)
    : base(getplayer, maxhp, damage)
    {

    }
    public override void OnAtack()
    {
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(player.transform.position, 1.1f);

        foreach (Collider2D collider in Enemies)
        {
            EnemyStats enemy = collider.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(PlayerDamage + (int)player.GetComponent<PlayerStats>().Spawner.GetComponent<AgressionController>().ReturnAgression() / 3);
            }
        }
    }
    public override void OnTakeDamege(IDamager atacker) {
        EventManager.AgressionUpdate(1);
    }
    public override void OnTakeHealth(int health) {
    base.OnTakeHealth(health);
        EventManager.AgressionUpdate(1);
    }
    public override void OnStart() { }
}

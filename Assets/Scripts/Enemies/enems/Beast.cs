using UnityEngine;

public class Beast : EnemyBase
{
    private int CurrentDamage;
    public Beast(EnemyStats enemy, int maxhp, int damage, int agressionpass, int speed, AudioClip deathclip)
: base(enemy, maxhp, damage, agressionpass, speed, deathclip)
    {

    }
    public override void OnAttack(GameObject player) 
    {
        player.GetComponent<PlayerStats>().TakeDamage(CurrentDamage);
        CurrentDamage = CurrentDamage * 2;
    }
    public override void OnSpecialAction() { }
}

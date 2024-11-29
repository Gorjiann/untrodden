using UnityEngine;

public class Thirst : EnemyBase
{
    public Thirst(EnemyStats enemy, int maxhp, int damage, int agressionpass, int speed, AudioClip deathclip)
: base(enemy, maxhp, damage, agressionpass, speed, deathclip)
    {

    }
    public override void OnAttack(GameObject player) 
    {
        EventManager.AgressionUpdate(Damage);
    }
    public override void OnSpecialAction() { }
}

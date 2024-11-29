using UnityEngine;

public class Skeleton : EnemyBase
{
    public Skeleton(EnemyStats enemy, int maxhp, int damage, int agressionpass, int speed, AudioClip deathclip)
: base(enemy, maxhp, damage, agressionpass, speed, deathclip)
    {

    }
    public override void OnSpecialAction() { }
}

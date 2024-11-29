using UnityEngine;

public class Guardian : EnemyBase
{
    private int CurrentDamage;
    private Torch spawntorch;
    public Guardian(EnemyStats enemy, int maxhp, int damage, int agressionpass, int speed, Torch torch, AudioClip deathclip)
: base(enemy, maxhp, damage, agressionpass, speed, deathclip)
    {
        CurrentDamage = damage;
        spawntorch = torch;
    }
    public override void OnSpecialAction() { }
    public override void OnSpawn() 
    {
        Torch newtorch = Instantiate(spawntorch);
        newtorch.transform.parent = thisenemy.transform;
    }
}
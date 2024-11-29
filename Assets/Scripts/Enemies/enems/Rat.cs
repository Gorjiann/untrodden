using UnityEngine;

public class Rat : EnemyBase
{
    public Rat(EnemyStats enemy, int maxhp, int damage, int agressionpass, int speed, AudioClip deathclip)
: base(enemy, maxhp, damage, agressionpass, speed, deathclip)
    {

    }
    public override void OnAttack(GameObject player) 
    {
        int chance = Random.Range(0, 4);
        if (player.GetComponent<PlayerInventory>().ItemsCount[chance] >= 0)
        {
            Debug.Log(chance);
            player.GetComponent<PlayerInventory>().Steal(chance);
           thisenemy.TakeDamage(100000);
        }

    }
    public override void OnSpecialAction() { }
}
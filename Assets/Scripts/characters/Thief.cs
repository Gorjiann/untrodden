using UnityEngine;

public class Thief : CharacterBase
{
    private int StartGold;
    public Thief (GameObject getplayer, int maxhp, int damage, int starttreasure)
    : base(getplayer, maxhp, damage)
    {
        StartGold = starttreasure;
    }
    public override void OnTakeDamege(IDamager atacker) 
    {
        if(player.CurrentTreasure > 0)
        {
            player.CurrentTreasure -= player.CurrentTreasure / 3 + 50;
            if(player.CurrentTreasure < 0)
                player.CurrentTreasure = 0;
        }
        else player.TakeDamage(atacker.Damage);
    }
    public override void OnTakeHealth(int health) { }
    public override void OnStart()
    {
        player.CurrentTreasure += StartGold;
    }
}

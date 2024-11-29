using UnityEngine;

public class Marauder : CharacterBase
{
    public Marauder(GameObject getplayer, int maxhp, int damage)
    : base(getplayer, maxhp, damage)
    {

    }
    public override void OnTakeTreasure() 
    {
        EventManager.AgressionUpdate(2);
        int rand = Random.Range(0, 10);
        int randitem = Random.Range(0, 4);
        if (rand >= 5)
        {
            player.GetComponent<PlayerInventory>().ItemsCount[randitem]++;
            player.GetComponent<PlayerInventory>().updatetext();
        }
    }
    public override void OnStart() { }
}

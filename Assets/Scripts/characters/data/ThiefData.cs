using UnityEngine;
[CreateAssetMenu(fileName = "Thief", menuName = "Characters/Thief")]
public class ThiefData : CharData
{
    public int StartGold;
    public override CharacterBase GetCharacter(GameObject player)
    {
        return new Thief(player, MaxHealhData, DamageData, StartGold);
    }
}

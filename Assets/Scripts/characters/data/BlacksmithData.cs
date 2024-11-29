using UnityEngine;
[CreateAssetMenu(fileName = "Blacksmith", menuName = "Characters/Blacksmith")]
public class BlacksmithData : CharData
{
    public override CharacterBase GetCharacter(GameObject player)
    {
        return new Blacksmith(player, MaxHealhData, DamageData);
    }
}

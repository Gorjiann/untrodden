using UnityEngine;
[CreateAssetMenu(fileName = "Marauder", menuName = "Characters/Marauder")]

public class MarauderData : CharData
{
    public override CharacterBase GetCharacter(GameObject player)
    {
        return new Marauder(player, MaxHealhData, DamageData);
    }
}

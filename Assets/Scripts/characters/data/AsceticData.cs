using UnityEngine;
[CreateAssetMenu(fileName = "Ascetic", menuName = "Characters/Ascetic")]
public class AsceticData : CharData
{
    public override CharacterBase GetCharacter(GameObject player)
    {
        return new Ascetic(player,MaxHealhData, DamageData);
    }
}

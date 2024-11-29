using UnityEngine;
[CreateAssetMenu(fileName = "Psychic", menuName = "Characters/Psychic")]
public class PsychicData : CharData
{
    public override CharacterBase GetCharacter(GameObject player)
    {
        return new Psychic(player, MaxHealhData, DamageData);
    }
}

using UnityEngine;
[CreateAssetMenu(fileName = "Undead", menuName = "Characters/Undead")]
public class UndeadData : CharData
{
    public int TurnsToRevial;
    public override CharacterBase GetCharacter(GameObject player)
    {
        return new Undead(player, MaxHealhData, DamageData, TurnsToRevial);
    }
}

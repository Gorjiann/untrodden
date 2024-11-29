using UnityEngine;
[CreateAssetMenu(fileName = "Paramedic", menuName = "Characters/Paramedic")]
public class ParamedicData : CharData
{
    public override CharacterBase GetCharacter(GameObject player)
    {
        return new Paramedic(player, MaxHealhData, DamageData);
    }
}

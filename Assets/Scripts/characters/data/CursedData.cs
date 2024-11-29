using UnityEngine;
[CreateAssetMenu(fileName = "Cursed", menuName = "Characters/Cursed")]

public class CursedData : CharData
{
    public override CharacterBase GetCharacter(GameObject player)
    {
        return new Cursed ( player,MaxHealhData, DamageData);
    }
}

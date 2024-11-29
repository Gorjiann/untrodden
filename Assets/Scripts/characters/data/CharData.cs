using UnityEngine;

public abstract class CharData : ScriptableObject
{
    public abstract CharacterBase GetCharacter(GameObject player);

    public int Index;

    public int MaxHealhData;
    public int DamageData;
    public Sprite CharacterIcon;
    public string CharacterDescription;
    public string CharacterName;
}

using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterController : MonoBehaviour
{

    [SerializeField] private CharacterList List;
    [SerializeField] private Image CharPortait;
    [SerializeField] public CharData CurrentCharacter;
    private void Awake()
    {
        CurrentCharacter = List.AllDatas[LevelGlobalPreference.CharacterIndex];
        CharPortait.sprite = CurrentCharacter.CharacterIcon;
        GetComponent<PlayerStats>().InstaliszeStats(CurrentCharacter.MaxHealhData);
        CurrentCharacter.GetCharacter(this.gameObject).OnStart();
    }
}

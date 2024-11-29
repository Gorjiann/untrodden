using UnityEngine;
using TMPro;

public class CharSelector : MonoBehaviour
{
    [SerializeField] private CharacterList chlist;
    [SerializeField] private TMP_Text CharacterName;
    [SerializeField] private TMP_Text Description;
    [SerializeField] private TMP_Text CharacterStats;
    [SerializeField] private TMP_Text TotalWorthText;
    [SerializeField] private TMP_Text  [] ItemsCountTexts;
    [SerializeField] private int[] ItemsCount;
    [SerializeField] private int TotalWorth;
    [SerializeField] private int[] ItemsCosts;

    private void Awake()
    {
        SelectCharacter(0);
        CleanItems();
    }
    public void SelectCharacter(int index)
    {
        CharacterStats.text = "Hp " + chlist.AllDatas[index].MaxHealhData.ToString() +" | " + "Damage " + chlist.AllDatas[index].DamageData.ToString();
        LevelGlobalPreference.CharacterIndex = index;
        CharacterName.text = chlist.AllDatas[index].CharacterName;
        Description.text = chlist.AllDatas[index].CharacterDescription;
    }

    public void IncreaceItemCount(int index)
    {
        if (TotalWorth - ItemsCosts[index] > 0)
        {
            TotalWorth -= ItemsCosts[index];
            ItemsCount[index]++;
            TotalWorthText.text = TotalWorth.ToString();
            ItemsCountTexts[index].text = ItemsCount[index].ToString();
            LevelGlobalPreference.ItemCounts[index]++;
        }
    }
    public void DecreaceItemCount(int index)
    {
        if (ItemsCount[index] > 1)
        {
            TotalWorth += ItemsCosts[index];
            ItemsCount[index]--;
            TotalWorthText.text = TotalWorth.ToString();
            ItemsCountTexts[index].text = ItemsCount[index].ToString();
            LevelGlobalPreference.ItemCounts[index]--;
        }
    }
    public void CleanItems()
    {
        for (int i = 0; i< ItemsCount.Length; i++)
        {
            ItemsCount[i] = 1;
            LevelGlobalPreference.ItemCounts[i] = 1;
        }
        TotalWorth = 900;
    }
}

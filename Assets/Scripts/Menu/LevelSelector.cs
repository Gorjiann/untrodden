using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private int LevelIndex;
    [SerializeField] private LevelData DefLevel;
    [SerializeField] private RectTransform Arrow;
    [SerializeField] private float ArrowX, ArrowY, ArrowOffset;
    [SerializeField] private GameObject[] RoomDescrips, EnemiesDescrips;
    [SerializeField] private TMP_Text GoalDescrips;

    private void Awake()
    {
        SelectLevel(0);
        ApplyLevelPrefs(DefLevel);
    }
    public void SelectLevel(int index)
    {
        LevelIndex = index;
        Arrow.localPosition = new Vector2(ArrowX + ArrowOffset*LevelIndex,ArrowY);
        LevelGlobalPreference.LevelIndex = LevelIndex;
    }
    public void ApplyLevelPrefs(LevelData data)
    {

        for (int i = 0; i < data.RoomsSpawnrateData.Length; i++)
        {
            LevelGlobalPreference.RoomsSpawnrate[i] = data.RoomsSpawnrateData[i];
        }
        for (int i = 0; i < data.EnemiesDescription.Length; i++)
        {
            LevelGlobalPreference.EnemySpawn[i] = data.EnemiesDescription[i];
        }
        for (int i = 0; i < RoomDescrips.Length; i++)
        {
            RoomDescrips[i].SetActive(data.RoomDescription[i]);
        }
        for (int i = 0; i < EnemiesDescrips.Length; i++)
        {
            EnemiesDescrips[i].SetActive(data.EnemiesDescription[i]);
        }
        GoalDescrips.text = data.GoalDescription;

    }
    public void LoadLevel()
    {
        LevelGlobalPreference.IsEndlees = false;
        SceneManager.LoadScene(1);
    }
    public void Quit() => Application.Quit();
}

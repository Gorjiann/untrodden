using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndlesslevelSelector : MonoBehaviour
{
    [SerializeField] private TMP_Text [] RoomsTexts;
    private int Enemytoadd;
  public void IncrRoomspawnrate(int roomindex)
    {
        if(LevelGlobalPreference.RoomsSpawnrate[roomindex]< 100)
        {
            LevelGlobalPreference.RoomsSpawnrate[roomindex] += 10;
            RoomsTexts[roomindex].text = LevelGlobalPreference.RoomsSpawnrate[roomindex].ToString();
        }

    }
    public void DecrRoomspawnrate(int roomindex)
    {
        if (LevelGlobalPreference.RoomsSpawnrate[roomindex] > 0)
        {
            LevelGlobalPreference.RoomsSpawnrate[roomindex] -= 10;
            RoomsTexts[roomindex].text = LevelGlobalPreference.RoomsSpawnrate[roomindex].ToString();
        }

    }
    public void SetEnemytolist(bool toggle)
    {
        LevelGlobalPreference.EnemySpawn[Enemytoadd] = toggle;
    }
    public void GetEnemyIndex(int index) {
    Enemytoadd = index;
    }
    public void LoadEndless()
    {
        LevelGlobalPreference.IsEndlees = true;
        SceneManager.LoadScene(1);
    }
}

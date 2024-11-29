using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public bool[] LevelDoneIndex;
    public GameObject[] DoneMarkers;

    private void Awake()
    {
        DataStorager data = SaveSystem.LoadLevelData();
        if (data != null && data.donebools != null)
        {
            for (int i = 0; i < DoneMarkers.Length; i++)
            {
                DoneMarkers[i].SetActive(data.donebools[i]);
            }
        }
    }

}

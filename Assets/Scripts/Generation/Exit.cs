using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour, IInteractable
{
    [SerializeField] public GameObject lightsource;
    private void Start()
    {

            EventManager.SetMagnetTarget(this.transform);
            lightsource.SetActive(true);
    }
    public void OnInteract(PlayerStats player)
    {
        SceneManager.LoadScene(0);
        if (!LevelGlobalPreference.IsEndlees)
        {
            SaveSystem.SaveLevelData(player);
        }
    }
}

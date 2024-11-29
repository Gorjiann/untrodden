using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] public int Lifetime;
    [SerializeField] private int LeftTurns;
    [SerializeField] private GameObject MainLight, SubLight;
    public bool IsEndless;
    private void Awake()
    {
        MainLight.SetActive(true);
        SubLight.SetActive(true);
        LeftTurns = Lifetime;
        EventManager.OnTurnPassed.AddListener(DecreaseLifetime);
    }

    public void DecreaseLifetime()
    {
        if (!IsEndless)
        {
            LeftTurns--;
        }

        if (LeftTurns <= 0)
            Destroy(gameObject);
    }
}

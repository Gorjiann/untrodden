using UnityEngine;

public class Treasures : MonoBehaviour, IInteractable
{

    [SerializeField] private int MinWorthness;
    [SerializeField] private int MaxWorthness;
    [SerializeField] public bool IsMeaning;
    [SerializeField] public GameObject lightsource;
    public int Parameter {  get; private set; }
    public int ReturnWorthness() => Random.Range(MinWorthness, MaxWorthness);

    private void Awake()
    {
        if (IsMeaning)
        {
            EventManager.SetMagnetTarget(this.transform);
            lightsource.SetActive(true);
        }

    }
    public void OnInteract(PlayerStats player)
    {
        player.TakeTreasure(ReturnWorthness(),IsMeaning);
        Destroy(gameObject);
    }
}

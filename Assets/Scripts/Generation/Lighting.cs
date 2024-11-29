using UnityEngine;
public class Lighting : MonoBehaviour
{
    [SerializeField] private bool[] SpawningDirections;
    [SerializeField] private bool Updater;
    private void Awake()
    {
        SendLightStand();
    }
    private void OnDestroy()
    {
        RemoveLightStand();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Room roomstate = collision.GetComponent<Room>();
        if (roomstate != null)
        {
            roomstate.CollapseNeighbor(SpawningDirections);
        }
        if (Updater) EventManager.UpdateTiless();
    }
    private void SendLightStand()
    {
        EventManager.AddLightingToList(this.transform);
    }
    private void RemoveLightStand()
    {
        EventManager.AddLightingToList(this.transform);
    }
}
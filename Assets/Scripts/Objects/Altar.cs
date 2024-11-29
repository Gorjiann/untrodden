using UnityEngine;

public class Altar : MonoBehaviour, IInteractable
{
    [SerializeField] private int Cost;
   public void OnInteract(PlayerStats player)
    {if(player.GetComponent<PlayerStats>().CurrentTreasure > Cost)
        {
            player.ActivateAltar(Cost);
        }

        Destroy(gameObject);
    }
}

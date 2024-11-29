using UnityEngine;

public class Trap : MonoBehaviour, IDamager
{
    public MonoBehaviour MonoRef => this;
    public int Damage { get; set; } = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage = 1;
        if (collision.GetComponent<PlayerStats>() != null)
        {
            collision.GetComponent<PlayerCharacterController>().CurrentCharacter.GetCharacter(collision.gameObject).OnTakeDamege(this);
            Destroy(gameObject);
        }
    }
}

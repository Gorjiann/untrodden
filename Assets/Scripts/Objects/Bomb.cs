using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject sublight;
    [SerializeField] private int Timer = 6;
    [SerializeField] private int Damage;
    private DungeonGenerator spawner;
    private AudioSource Source;
    [SerializeField] private AudioClip Explose;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        Source = GetComponent<AudioSource>();   
        spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.OnTurnPassed.AddListener(Ticking);
    }
    public void SetSpawner(DungeonGenerator _spawner)
    {
        spawner = _spawner;
    }
    private void Ticking()
    {
        Timer--;
            if(Timer == 3)
        {
            Source.PlayOneShot(Explose);
            Collider2D[] Enemies = Physics2D.OverlapCircleAll(transform.position, 2.5f);

            foreach (Collider2D collider in Enemies)
            {
                EnemyStats enemy = collider.GetComponent<EnemyStats>();
                if (enemy != null)
                {
                    enemy.TakeDamage(Damage);
                }
            }
            spawner.SpawnedRooms[(int)transform.position.x, (int)transform.position.y].EntangleTile();
            spawner.SpawnedRooms[(int)transform.position.x, (int)transform.position.y].CollapseAsStart();
            sublight.SetActive(true);
            spriteRenderer.sprite = null;

        }
        if (Timer <= 0)
            Destroy(gameObject);
    }
}

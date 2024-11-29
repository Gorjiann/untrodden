using UnityEngine;

public class Hunter : EnemyBase
{
    private Trap spawntrap;
    private EnemyStats enemypos;
    private EnemyStats skeleton;
    private EnemyData skeletondata;
    public Hunter(EnemyStats enemy, int maxhp, int damage, int agressionpass, int speed, EnemyStats weakenem, EnemyData enemdata, Trap trap, AudioClip deathclip)
: base(enemy, maxhp, damage, agressionpass, speed, deathclip)
    {
        enemypos = enemy;
        spawntrap = trap;
        skeleton = weakenem;
        skeletondata = enemdata;
    }
    public override void OnAttack(GameObject player)
    {
        player.GetComponent<PlayerStats>().TakeDamage(1);
        EnemyStats newenem = Instantiate(skeleton, enemypos.gameObject.transform.position, Quaternion.identity);
        newenem.Initialize(enemypos.gameObject.transform, new Vector2Int((int)newenem.transform.position.x,(int) transform.position.y), skeletondata);
        newenem.AgressionController = enemypos.AgressionController;
        newenem.Generator = enemypos.Generator;
    }
    public override void OnSpecialAction()
    {
        int chanse = Random.Range(1, 90);
        if (chanse > 80)
        {
            Trap newtrap = Instantiate(spawntrap, enemypos.gameObject.transform.position, Quaternion.identity);
        }
    }
}

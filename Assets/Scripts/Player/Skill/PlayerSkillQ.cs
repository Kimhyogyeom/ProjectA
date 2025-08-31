using UnityEngine;

public class PlayerSkillQ : MonoBehaviour
{
    [SerializeField] private PlayerSkillController playerSkillController;
    [SerializeField] private PlayerStats playerStats;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enumy")
        {
            EnemyStats enumyStats = other.gameObject.GetComponent<EnemyStats>();
            enumyStats?.TakeDamage(playerStats.playerDamage * playerStats.playerSkillQDamage);

            playerSkillController.ResetSkillObject();
        }
        if (other.tag == "Golem")
        {
            GolemAI golemAI = other.gameObject.GetComponent<GolemAI>();
            golemAI?.TakeDamage(playerStats.playerDamage * playerStats.playerSkillQDamage);

            playerSkillController.ResetSkillObject();
        }
        if (other.tag == "TurretEnemy")
        {
            TurretStats turretStat = other.gameObject.GetComponent<TurretStats>();
            turretStat?.TakeDamage(playerStats.playerDamage * playerStats.playerSkillQDamage);
        }
        if (other.tag == "EnemyNexus")
        {
            NexusStats nexusStat = other.gameObject.GetComponent<NexusStats>();
            nexusStat?.TakeDamage(playerStats.playerDamage * playerStats.playerSkillQDamage);
        }
    }
}

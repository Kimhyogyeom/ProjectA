using UnityEngine;

public class PlayerTargetObject : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform targetEnumyTr;
    [SerializeField] private Rigidbody basicAttackRb;
    [SerializeField] private PlayerStats playerStats;

    [Header("Setting Value")]
    [SerializeField] private float basicAttackSpeed = 0f;

    void FixedUpdate()
    {
        if (targetEnumyTr != null)
        {
            if (targetEnumyTr.gameObject.tag == "EnemyNexus")
            {
                Vector3 direction = (targetEnumyTr.position + new Vector3(0, 2, 0)) - transform.position;
                basicAttackRb.velocity = direction.normalized * basicAttackSpeed;
            }
            else if (targetEnumyTr.gameObject.tag == "TurretEnemy")
            {
                Vector3 direction = (targetEnumyTr.position + new Vector3(0, 2, 0)) - transform.position;
                basicAttackRb.velocity = direction.normalized * basicAttackSpeed;
            }
            else
            {
                Vector3 direction = targetEnumyTr.position - transform.position;
                basicAttackRb.velocity = direction.normalized * basicAttackSpeed;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void SetTarget(Transform newTarget)
    {
        targetEnumyTr = newTarget;
    }
    void OnTriggerEnter(Collider other)
    {
        if (targetEnumyTr != null && ReferenceEquals(other.gameObject, targetEnumyTr.gameObject) && other.tag == "Enumy")
        {
            EnemyStats enumyStats = targetEnumyTr.GetComponent<EnemyStats>();
            enumyStats?.TakeDamage(playerStats.playerDamage);
            targetEnumyTr = null;
            this.gameObject.SetActive(false);
        }
        if (targetEnumyTr != null && ReferenceEquals(other.gameObject, targetEnumyTr.gameObject) && other.tag == "Golem")
        {
            targetEnumyTr.GetComponent<GolemAI>().TakeDamage(playerStats.playerDamage);
            targetEnumyTr = null;
            this.gameObject.SetActive(false);
        }
        if (targetEnumyTr != null && ReferenceEquals(other.gameObject, targetEnumyTr.gameObject) && other.tag == "TurretEnemy")
        {
            targetEnumyTr.GetComponent<TurretStats>().TakeDamage(playerStats.playerDamage);
            targetEnumyTr = null;
            this.gameObject.SetActive(false);
        }
        if (targetEnumyTr != null && ReferenceEquals(other.gameObject, targetEnumyTr.gameObject) && other.tag == "EnemyNexus")
        {
            targetEnumyTr.GetComponent<NexusStats>().TakeDamage(playerStats.playerDamage);
            targetEnumyTr = null;
            this.gameObject.SetActive(false);
        }
    }
}
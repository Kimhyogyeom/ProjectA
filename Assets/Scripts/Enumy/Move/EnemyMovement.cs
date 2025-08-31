using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private Animator enumyAnim;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private string minionTag = "EnemyMinion";
    [SerializeField] private string turretTag = "Turret";

    [Header("Targeting Settings")]
    [SerializeField] private float stopDistance = 2.0f;
    [SerializeField] private float aggroRange = 5.0f;
    [SerializeField] private float targetSwitchInterval = 2.0f;

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 1.5f;   // 공격 간격 (초)
    private float lastAttackTime = 0f;                      // 마지막 공격 시각

    private Transform currentTarget;
    private float timeSinceLastTargetSwitch = 0.0f;

    [SerializeField] private string nexusTag; // "EnemyNexus" 같은 태그 입력
    private GameObject nexusObj;

    void Start()
    {
        agent.stoppingDistance = stopDistance;

        // 태그로 넥서스 찾기
        if (!string.IsNullOrEmpty(nexusTag))
        {
            nexusObj = GameObject.FindGameObjectWithTag(nexusTag);
        }

        UpdateTarget();
    }

    void Update()
    {
        timeSinceLastTargetSwitch += Time.deltaTime;

        if (timeSinceLastTargetSwitch >= targetSwitchInterval)
        {
            UpdateTarget();
            timeSinceLastTargetSwitch = 0.0f;
        }

        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);

            float distance = Vector3.Distance(transform.position, currentTarget.position);

            // 넥서스일 때만 stopDistance를 2배로 늘려서 공격
            float effectiveStopDistance = stopDistance;
            if (currentTarget.gameObject == nexusObj)
            {
                effectiveStopDistance *= 2f; // 필요에 따라 거리 조절
            }

            if (distance <= effectiveStopDistance)
            {
                RotateTowards(currentTarget);
                TryAttack();
            }
        }
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0;

        if (direction.magnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            if (currentTarget.CompareTag(minionTag))
            {
                currentTarget.GetComponent<EnemyStats>().TakeDamageEnemy(enemyStats.enemyDamage);
            }
            else if (currentTarget.CompareTag(turretTag))
            {
                currentTarget.GetComponent<TurretStats>().TakeDamage(enemyStats.enemyDamage);
            }
            else if (currentTarget.gameObject == nexusObj)
            {
                currentTarget.GetComponent<NexusStats>().TakeDamage(enemyStats.enemyDamage);
            }

            enumyAnim.SetTrigger("IsAttack");
        }
    }

    private void UpdateTarget()
    {

        Transform closestMinion = GetClosestObjectInRadius(GameObject.FindGameObjectsWithTag(minionTag), aggroRange);

        if (closestMinion != null)
        {
            currentTarget = closestMinion;
        }
        else
        {
            Transform closestTurret = GetClosestObject(GameObject.FindGameObjectsWithTag(turretTag));

            if (closestTurret != null)
            {
                currentTarget = closestTurret;
            }
            else
            {
                currentTarget = nexusObj != null ? nexusObj.transform : null;
            }
        }
    }

    private Transform GetClosestObject(GameObject[] objects)
    {
        float closestDistance = Mathf.Infinity;
        Transform closestObject = null;
        Vector3 currentPosition = transform.position;

        foreach (var obj in objects)
        {
            float distance = Vector3.Distance(currentPosition, obj.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj.transform;
            }
        }
        return closestObject;
    }

    private Transform GetClosestObjectInRadius(GameObject[] objects, float radius)
    {
        float closestDistance = Mathf.Infinity;
        Transform closestObject = null;
        Vector3 currentPosition = transform.position;

        foreach (var obj in objects)
        {
            float distance = Vector3.Distance(currentPosition, obj.transform.position);

            if (distance < closestDistance && distance <= radius)
            {
                closestDistance = distance;
                closestObject = obj.transform;
            }
        }
        return closestObject;
    }
}

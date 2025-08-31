using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GolemAI : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private GolemStats golemStats;
    [SerializeField] private GolemHpBar golemHpBar;

    [Header("전투 관련")]
    [SerializeField] private float attackCD = 3f;       // 공격 쿨다운
    [SerializeField] private float attackRange = 1f;    // 공격 범위
    [SerializeField] private float aggroRange = 4f;     // 어그로 범위
    [SerializeField] private float maxRange = 10f;      // 최대 활동 반경

    [Header("참조")]
    [SerializeField] private GameObject player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Collider goleCollider;

    [Header("스폰 정보")]
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private Quaternion spawnRotation;

    float timePassed;
    float newDestinationCD = 0.5f;

    enum GolemState { Idle, Chase, Attack, Return, Die }
    [SerializeField] private GolemState currentState = GolemState.Idle;

    void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }

    void Update()
    {
        if (currentState == GolemState.Die) return;

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        float distanceToSpawn = Vector3.Distance(transform.position, spawnPosition);

        if (distanceToSpawn > maxRange)
        {
            currentState = GolemState.Return;
            aggroRange = 0f;
        }
        else if (distanceToPlayer <= attackRange)
        {
            currentState = GolemState.Attack;
        }
        else if (distanceToPlayer <= aggroRange)
        {
            currentState = GolemState.Chase;
        }
        else if (distanceToSpawn > 0.1f)
        {
            currentState = GolemState.Return;
        }
        else
        {
            currentState = GolemState.Idle;
            aggroRange = 5.5f;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        // --- 상태별 행동 ---
        switch (currentState)
        {
            case GolemState.Attack:
                if (timePassed >= attackCD)
                {
                    animator.SetTrigger("IsAttack");
                    timePassed = 0f;
                }
                agent.SetDestination(transform.position);
                transform.LookAt(player.transform);
                break;

            case GolemState.Chase:
                if (newDestinationCD <= 0)
                {
                    newDestinationCD = 0.5f;
                    agent.SetDestination(player.transform.position);
                }
                transform.LookAt(player.transform);
                break;

            case GolemState.Return:
                if (newDestinationCD <= 0)
                {
                    newDestinationCD = 0.5f;
                    agent.SetDestination(spawnPosition);
                }
                transform.LookAt(spawnPosition);
                break;

            case GolemState.Idle:
                agent.SetDestination(transform.position);

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    spawnRotation,
                    Time.deltaTime * 2f
                );

                if (golemStats.golemHp < golemStats.golemMaxHp)
                {
                    golemStats.golemHp = Mathf.Min(
                        golemStats.golemHp + 1f * Time.deltaTime,
                        golemStats.golemMaxHp
                    );
                    golemHpBar.HpVarUpdate(golemStats.golemHp, golemStats.golemMaxHp);
                }
                break;
        }

        timePassed += Time.deltaTime;
        newDestinationCD -= Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        golemStats.golemHp -= damage;
        golemHpBar.HpVarUpdate(golemStats.golemHp, golemStats.golemMaxHp);
        if (golemStats.golemHp <= 0)
        {
            goleCollider.enabled = false;
            agent.enabled = false;
            playerAttack.enumyGameobject = null;
            playerMovement.enumyGameobject = null;
            animator.SetTrigger("IsDie");
            currentState = GolemState.Die;

            StartCoroutine(GolemDieProcess());
        }
    }
    private IEnumerator GolemDieProcess()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        // 공격 범위 (빨강)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // 어그로 범위 (노랑)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

        // 최대 활동 범위 (초록)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPosition, maxRange);
    }
}

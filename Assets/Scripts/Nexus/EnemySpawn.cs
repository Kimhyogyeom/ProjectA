using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;

    [SerializeField] private Animator nexusAnim;

    [Header("Enemy Settings")]
    [SerializeField] private float enemyMoveSpeed = 3.5f;
    [SerializeField] private float superEnemyMoveSpeed = 5.0f;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject superEnemyPrefab;
    [SerializeField] private GameObject targetNexus;
    [SerializeField] private GameObject[] targetTurrets;

    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 20.0f;
    [SerializeField] private int enemiesPerWave = 6;
    [SerializeField] private int wavesUntilSuperEnemy = 3;
    [SerializeField] private float delayBetweenEnemies = 1.5f;

    [SerializeField] private int waveCount = 0;


    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (GameManager.Instance.gameState == GameManager.GameState.Stop) break;

            if (GameManager.Instance.gameState == GameManager.GameState.Ready)
            {
                yield return null;
                continue;
            }
            waveCount++;

            bool isSuperWave = (waveCount % wavesUntilSuperEnemy == 0);

            int regularCount = isSuperWave ? enemiesPerWave - 1 : enemiesPerWave;

            for (int i = 0; i < regularCount; i++)
            {
                SpawnEnemy(enemyPrefab, enemyMoveSpeed);
                nexusAnim.SetTrigger("IsAttack");
                yield return new WaitForSeconds(delayBetweenEnemies);
            }

            if (isSuperWave)
            {
                nexusAnim.SetTrigger("IsAttack");
                SpawnEnemy(superEnemyPrefab, superEnemyMoveSpeed);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(GameObject prefab, float speed)
    {
        if (prefab == null || spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        EnemyManager.Instance.RegisterMinion(enemy.gameObject);
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetTargetNexus(targetNexus);
            enemyMovement.SetTargetTurret(targetTurrets);
        }
        else
        {
            Debug.Log($"EnemyMovement check Go");
        }
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = speed;
        }
        else
        {
            Debug.Log($"NavMeshAgent check Go");
        }
    }
}

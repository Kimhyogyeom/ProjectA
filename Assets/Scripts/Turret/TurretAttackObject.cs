using Unity.VisualScripting;
using UnityEngine;

public class TurretAttackObject : MonoBehaviour
{
    [SerializeField] private TurretStats turrentStats;
    [SerializeField] private float moveSpeed = 10.0f;
    private Transform targetTransform;

    [SerializeField] private GameObject playerObj;
    public void Initialize(Transform target)
    {
        targetTransform = target;
    }

    private void Update()
    {
        if (targetTransform == null)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 direction = targetTransform.position - transform.position;
        float step = moveSpeed * Time.deltaTime;

        if (direction.magnitude <= step)
        {
            HitTarget(targetTransform.gameObject);
            return;
        }

        transform.Translate(direction.normalized * step, Space.World);
    }

    private void HitTarget(GameObject target)
    {
        EnemyStats enemy = target.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            enemy.TakeDamageTurret(turrentStats.damage);
        }
        else
        {
            PlayerStats player = target.GetComponent<PlayerStats>();
            if (player != null && player.tag == "Player")
            {
                player.TakeDamage(turrentStats.damage);
            }
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == targetTransform)
        {
            other.GetComponent<EnemyStats>().TakeDamageTurret(turrentStats.damage);
            gameObject.SetActive(false);
        }
        if (other.gameObject == targetTransform)
        {
            other.GetComponent<PlayerStats>().TakeDamage(turrentStats.damage);
            gameObject.SetActive(false);
        }
    }
}

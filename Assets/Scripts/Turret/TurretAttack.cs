using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private TurretAttackObject turretAttackObject;
    [SerializeField] private string minionTag;
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 5.0f;
    [SerializeField] private float attackInterval = 2.0f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    [Header("Visuals")]
    [SerializeField] private LineRenderer targetingLine;

    [SerializeField] private Transform player;

    private float nextAttackTime = 0f;
    private GameObject currentTarget;

    private void Update()
    {
        if (currentTarget == null || Vector3.Distance(transform.position, currentTarget.transform.position) > attackRange)
        {
            AcquireTarget();
        }

        UpdateTargetingLine();

        if (currentTarget != null && Time.time >= nextAttackTime)
        {
            FireAtTarget();
            nextAttackTime = Time.time + attackInterval;
        }
    }

    private void AcquireTarget()
    {
        GameObject nearestTarget = null;
        float closestDistance = float.MaxValue;

        if (player != null && player.tag == "Player")
        {
            float playerDistance = Vector3.Distance(transform.position, player.position);
            if (playerDistance <= attackRange)
            {
                nearestTarget = player.gameObject;
                closestDistance = playerDistance;
            }
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(minionTag);
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= attackRange && distance < closestDistance)
            {
                closestDistance = distance;
                nearestTarget = enemy;
            }
        }

        currentTarget = nearestTarget;
    }

    private void UpdateTargetingLine()
    {
        if (currentTarget != null)
        {
            targetingLine.enabled = true;
            targetingLine.SetPosition(0, firePoint.position);
            targetingLine.SetPosition(1, currentTarget.transform.position);
        }
        else
        {
            targetingLine.enabled = false;
        }
    }

    private void FireAtTarget()
    {
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = Quaternion.identity;
        projectile.gameObject.SetActive(true);
        if (projectile != null)
        {
            turretAttackObject.Initialize(currentTarget.transform);
        }
    }
}

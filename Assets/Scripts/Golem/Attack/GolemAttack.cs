using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GolemStats golemStats;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerStats.TakeDamage(golemStats.golemDamage);
        }
    }
}

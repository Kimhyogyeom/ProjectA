using UnityEngine;

public class NexusController : MonoBehaviour
{
    public int turretCount = 0;
    public int enemyTurretCount = 0;

    [SerializeField] GameObject turretHpBarObj;
    [SerializeField] GameObject enemyTurrethpBarObj;

    public void TurretCountUp()
    {
        turretCount++;
        if (enemyTurretCount >= 3)
        {
            NexusHpController();
        }
    }
    public void EnemyTurretCountUp()
    {
        enemyTurretCount++;
        if (enemyTurretCount >= 3)
        {
            EnemyNexusHpController();
        }
    }
    private void NexusHpController()
    {
        turretHpBarObj.SetActive(true);
    }

    private void EnemyNexusHpController()
    {
        enemyTurrethpBarObj.SetActive(true);
    }

}

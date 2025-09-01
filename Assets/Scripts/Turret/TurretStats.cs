using UnityEngine;

public class TurretStats : MonoBehaviour
{
    public float damage = 5.0f;

    public float turretMaxHp = 200.0f;
    public float turretHp = 0f;
    [SerializeField] private TurretHpBar turretHpBar;

    [SerializeField] private TurretDestory turretDestory;
    [SerializeField] private string classification = "";
    [SerializeField] private int number;

    [SerializeField] private NexusController nexusController;
    [SerializeField] private GameObject nexusBoomEffect;
    void Start()
    {
        turretMaxHp = turretHp;
    }

    public void TakeDamage(float damage)
    {
        turretHp -= damage;
        turretHpBar.HpVarUpdate(turretHp, turretMaxHp);
        if (turretHp <= 0)
        {
            if (classification == "EnemyTurret")
            {
                turretDestory.EnemyTurretDestoryIcon(number);
                nexusController.EnemyTurretCountUp();
            }
            else if (classification == "Turret")
            {
                turretDestory.TurretDestoryIcon(number);
                nexusController.TurretCountUp();
            }

            nexusBoomEffect.SetActive(true);
            nexusBoomEffect.GetComponent<ParticleSystem>().Play();
            SoundManager.Instance.PlaySfxUI(SoundManager.Instance.soundDatabase.turretBreakClip);
            Destroy(this.gameObject);
        }
    }
}